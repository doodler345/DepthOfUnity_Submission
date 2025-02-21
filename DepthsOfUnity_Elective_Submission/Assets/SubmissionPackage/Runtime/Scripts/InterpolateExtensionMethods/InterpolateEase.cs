using UnityEngine;

namespace InterpolateExtentionMethods
{
    public enum EaseType
    {
        Linear,
        InSine,
        OutSine,
        InOutSine,
    }

    [CreateAssetMenu(fileName = "InterpolateEase", menuName = "InterpolateExtensionMethods/InterpolateEase")]
    public class InterpolateEase : ScriptableObject
    {
        static InterpolateEase instance;

        public static InterpolateEase Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Resources.Load<InterpolateEase>("Data/InterpolateExtensionMethods/InterpolateEaseData");
                }
                return instance;
            }
        }

        public AnimationCurve GetEase(EaseType easeType)
        {
            switch (easeType)
            {
                case EaseType.InSine:
                    return inSine;
                case EaseType.OutSine:
                    return outSine;
                case EaseType.Linear:
                    return linear;
                case EaseType.InOutSine:
                    return inOutSine;
                default:
                    return linear;
            }
        }

        [SerializeField] private AnimationCurve linear;
        [SerializeField] private AnimationCurve inSine;
        [SerializeField] private AnimationCurve outSine;
        [SerializeField] private AnimationCurve inOutSine;
    }
}
