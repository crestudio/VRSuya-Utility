#if UNITY_EDITOR
using UnityEngine;

using VRC.SDKBase;

/*
 * VRSuya Utility
 * Contact : vrsuya@gmail.com // Twitter : https://twitter.com/VRSuya
 */

namespace VRSuya.Utility {

	[AddComponentMenu("VRSuya/VRSuya GlobalTransform")]
	[HelpURL("https://vrsuya.booth.pm/")]
	[RequireComponent(typeof(Transform))]
	public class GlobalTransform : MonoBehaviour, IEditorOnly { }
}
#endif