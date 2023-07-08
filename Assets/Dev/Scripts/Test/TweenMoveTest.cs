using System.Threading.Tasks;
using UnityEngine;

namespace Dev.Scripts.Test
{
    public class TweenMoveTest : MonoBehaviour
    {
        private async void Start()
        {
            await Task.Delay(1000);
            var tweenMove = transform.TweenMove(new Vector3(2, 2, 2), new Vector3(35, 45, 90), 0.1f);
            StartCoroutine(tweenMove);
        }
    }
}