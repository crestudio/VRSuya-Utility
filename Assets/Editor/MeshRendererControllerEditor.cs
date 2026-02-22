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
		static void UpdateAvatarRenders() {
			MeshRendererController MeshRendererControllerInstance = CreateInstance<MeshRendererController>();
			MeshRendererControllerInstance.RequestUpdateAvatarRenders();
		}

		[MenuItem("Tools/VRSuya/Utility/MeshRenderer/Adjust Bound Box", priority = 1100)]
		static void UpdateBounds() {
			MeshRendererController MeshRendererControllerInstance = CreateInstance<MeshRendererController>();
			MeshRendererControllerInstance.RequestUpdateBounds();
		}

		[MenuItem("Tools/VRSuya/Utility/MeshRenderer/Change to Two-Sided Shadow", priority = 1100)]
		static void UpdateTwosidedShadow() {
			MeshRendererController MeshRendererControllerInstance = CreateInstance<MeshRendererController>();
			MeshRendererControllerInstance.RequestUpdateTwosidedShadow();
		}

		[MenuItem("Tools/VRSuya/Utility/MeshRenderer/Change Probes Settings", priority = 1100)]
		static void UpdateProbes() {
			MeshRendererController MeshRendererControllerInstance = CreateInstance<MeshRendererController>();
			MeshRendererControllerInstance.RequestUpdateProbes();
		}

		[MenuItem("Tools/VRSuya/Utility/MeshRenderer/Assign AnchorOverride", priority = 1100)]
		static void UpdateAnchorOverride() {
			MeshRendererController MeshRendererControllerInstance = CreateInstance<MeshRendererController>();
			MeshRendererControllerInstance.RequestUpdateAnchorOverride();
		}
	}
}
#endif