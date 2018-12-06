using System;
using UnityEngine;
using UnityOSC;

[RequireComponent(typeof(SkinnedMeshRenderer))]
public class OSCARKitFacialTracker : OSCFilter {
    
    [SerializeField] string prefix = "any_blendshape.";
    [SerializeField] bool log;

    public float strengthMultiplier = 1;

    private SkinnedMeshRenderer meshTarget;
    

    protected override void Start()
    {
        OSCManager.Instance.FilterManager.Filters.Add(this);

        meshTarget = GetComponent<SkinnedMeshRenderer>();
    }

    public override void OnReceivedOSC(OSCMessage message)
    {
        foreach (string msg in message.Data[0].ToString().Split(new Char[] { '|' }))
        {
            var cleanString = msg.Replace(" ", "").Replace("msg:", "");
            var strArray = cleanString.Split(new Char[] { '-' });

            if (strArray.Length == 2)
            {
                var weight = float.Parse(strArray.GetValue(1).ToString());

                var mappedShapeName = strArray.GetValue(0).ToString().Replace("_L", "Left").Replace("_R", "Right");

                var index = meshTarget.sharedMesh.GetBlendShapeIndex(prefix + mappedShapeName);

                //if (mappedShapeName.Contains("mouth"))
                //{
                //    weight *=3;
                //}

                if (index > -1)
                {
                    meshTarget.SetBlendShapeWeight(index, weight * strengthMultiplier);
                }
            }
        }
    }

}
