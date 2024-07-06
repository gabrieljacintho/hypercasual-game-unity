using Bits.Physics;
using UnityEngine;

namespace Bits.GameObjectStack
{
    public class StackDetector : GenericDetector<GameObjectStack>
    {
        [Header("Stack")]
        [SerializeField] private bool _onlyNotEmpty;


        protected override bool CanDetectComponent(GameObjectStack component)
        {
            return base.CanDetectComponent(component) && (!_onlyNotEmpty || component.Count > 0);
        }
    }
}