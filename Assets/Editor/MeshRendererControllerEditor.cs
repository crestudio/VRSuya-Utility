#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

/*
 * VRSuya Utility
 * Contact : vrsuya@gmail.com // Twitter : https://twitter.com/VRSuya
 */

namespace com.vrsuya.utility {

	[ExecuteInEditMode]
	public class MeshRendererControllerEditor : EditorWindow {

		[MenuItem("Tools/VRSuya/Utility/MeshRenderer/Update Renderer Setting", priority = 1000)]
		public static void UpdateAvatarRenders() {
			MeshRendererController MeshRendererControllerInstance = CreateInstance<MeshRendererController>();
			MeshRendererControllerInstance.RequestUpdateAvatarRenders();
			return;
		}

		[MenuItem("Tools/VRSuya/Utility/MeshRenderer/Adjust Bound Box", priority = 1100)]
		public static void UpdateBounds() {
			MeshRendererController MeshRendererControllerInstance = CreateInstance<MeshRendererController>();
			MeshRendererControllerInstance.RequestUpdateBounds();
			return;
		}

		[MenuItem("Tools/VRSuya/Utility/MeshRenderer/Change to Two-Sided Shadow", priority = 1100)]
		public static void UpdateTwosidedShadow() {
			MeshRendererController MeshRendererControllerInstance = CreateInstance<MeshRendererController>();
			MeshRendererControllerInstance.RequestUpdateTwosidedShadow();
			return;
		}

		[MenuItem("Tools/VRSuya/Utility/MeshRenderer/Change Probes Settings", priority = 1100)]
		public static void UpdateProbes() {
			MeshRendererController MeshRendererControllerInstance = CreateInstance<MeshRendererController>();
			MeshRendererControllerInstance.RequestUpdateProbes();
			return;
		}

		[MenuItem("Tools/VRSuya/Utility/MeshRenderer/Assign AnchorOverride", priority = 1100)]
		public static void UpdateAnchorOverride() {
			MeshRendererController MeshRendererControllerInstance = CreateInstance<MeshRendererController>();
			MeshRendererControllerInstance.RequestUpdateAnchorOverride();
			return;
		}
	}
}
#endif