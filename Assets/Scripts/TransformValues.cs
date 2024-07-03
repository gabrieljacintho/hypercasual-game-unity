using UnityEngine;

namespace Bits
{
    public struct TransformValues
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Scale;


        public TransformValues(Transform transform, Space space = Space.Self)
        {
            if (space == Space.Self)
            {
                Position = transform.localPosition;
                Rotation = transform.localRotation;
            }
            else
            {
                Position = transform.position;
                Rotation = transform.rotation;
            }

            Scale = transform.localScale;
        }

        public TransformValues(Vector3 position)
        {
            Position = position;
            Rotation = Quaternion.identity;
            Scale = Vector3.one;
        }

        public TransformValues(Vector3 position, Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
            Scale = Vector3.one;
        }

        public TransformValues(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
        }
    }
}