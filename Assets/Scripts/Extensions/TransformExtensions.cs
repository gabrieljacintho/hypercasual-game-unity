using System.Collections.Generic;
using UnityEngine;

namespace Bits.Extensions
{
    public static class TransformExtensions
    {
        public static TransformValues Values(this Transform transform)
        {
            return new TransformValues(transform);
        }

        public static Dictionary<Transform, TransformValues> AllValues(this Transform transform)
        {
            Dictionary<Transform, TransformValues> transformValues = new();

            transformValues.Add(transform, transform.Values());

            foreach (Transform child in transform)
            {
                Dictionary<Transform, TransformValues> childValues = AllValues(child);
                transformValues.AddRange(childValues);
            }

            return transformValues;
        }

        public static void SetValues(this Transform transform, TransformValues values, Space space = Space.Self)
        {
            if (space == Space.Self)
            {
                transform.localPosition = values.Position;
                transform.localRotation = values.Rotation;
            }
            else
            {
                transform.position = values.Position;
                transform.rotation = values.Rotation;
            }

            transform.localScale = values.Scale;
        }

        public static void LoadValues(this Dictionary<Transform, TransformValues> values, Space space = Space.Self)
        {
            foreach (KeyValuePair<Transform, TransformValues> transformValue in values)
            {
                if (transformValue.Key != null)
                {
                    transformValue.Key.SetValues(transformValue.Value, space);
                }
            }
        }
    }
}