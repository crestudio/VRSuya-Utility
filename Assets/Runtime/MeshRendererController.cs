#if UNITY_EDITOR
using System.Linq;

using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

using VRC.SDKBase;

using VRSuya.Core;

/*
 * VRSuya Utility
 * Contact : vrsuya@gmail.com // Twitter : https://twitter.com/VRSuya
 */

namespace VRSuya.Utility {

	public class MeshRendererController : ScriptableObject {

		const string UndoGroupName = "VRSuya MeshRendererController";
		int UndoGroupIndex;

		public void RequestUpdateAvatarRenders() {
			VRC_AvatarDescriptor AvatarDescriptor = AvatarUtility.GetAvatarDescriptor();
			if (AvatarDescriptor) {
				UndoGroupIndex = UnityUtility.InitializeUndoGroup(UndoGroupName);
				GameObject AvatarGameObject = AvatarDescriptor.gameObject;
				Transform AvatarAnchorOverride = AvatarUtility.GetAvatarAnchorOverride(AvatarGameObject);
				if (!AvatarAnchorOverride) AvatarAnchorOverride = GetAnchorOverride(AvatarGameObject);
				(SkinnedMeshRenderer[] AvatarSkinnedMeshRenderers, MeshRenderer[] AvatarMeshRenderers) = GetAvatarRenderers(AvatarGameObject);
				Bounds NewBounds = new Bounds {
					center = new Vector3(0.0f, 0.0f, 0.0f),
					extents = new Vector3(1.0f, 1.0f, 1.0f),
				};
				foreach (SkinnedMeshRenderer TargetSkinnedMeshRenderer in AvatarSkinnedMeshRenderers) {
					if (TargetSkinnedMeshRenderer.localBounds != NewBounds) {
						Undo.RecordObject(TargetSkinnedMeshRenderer, UndoGroupName);
						TargetSkinnedMeshRenderer.updateWhenOffscreen = true;
						TargetSkinnedMeshRenderer.localBounds = NewBounds;
						TargetSkinnedMeshRenderer.updateWhenOffscreen = false;
						EditorUtility.SetDirty(TargetSkinnedMeshRenderer);
						Undo.CollapseUndoOperations(UndoGroupIndex);
					}
					if (TargetSkinnedMeshRenderer.shadowCastingMode != ShadowCastingMode.TwoSided) {
						Undo.RecordObject(TargetSkinnedMeshRenderer, UndoGroupName);
						TargetSkinnedMeshRenderer.shadowCastingMode = ShadowCastingMode.TwoSided;
						Undo.CollapseUndoOperations(UndoGroupIndex);
					}
					if (TargetSkinnedMeshRenderer.lightProbeUsage != LightProbeUsage.BlendProbes) {
						Undo.RecordObject(TargetSkinnedMeshRenderer, UndoGroupName);
						TargetSkinnedMeshRenderer.lightProbeUsage = LightProbeUsage.BlendProbes;
						Undo.CollapseUndoOperations(UndoGroupIndex);
					}
					if (TargetSkinnedMeshRenderer.reflectionProbeUsage != ReflectionProbeUsage.Off) {
						Undo.RecordObject(TargetSkinnedMeshRenderer, UndoGroupName);
						TargetSkinnedMeshRenderer.reflectionProbeUsage = ReflectionProbeUsage.Off;
						Undo.CollapseUndoOperations(UndoGroupIndex);
					}
					if (TargetSkinnedMeshRenderer.probeAnchor != AvatarAnchorOverride) {
						Undo.RecordObject(TargetSkinnedMeshRenderer, UndoGroupName);
						TargetSkinnedMeshRenderer.probeAnchor = AvatarAnchorOverride;
						Undo.CollapseUndoOperations(UndoGroupIndex);
					}
				}
				foreach (MeshRenderer TargetMeshRenderer in AvatarMeshRenderers) {
					if (TargetMeshRenderer.shadowCastingMode != ShadowCastingMode.TwoSided) {
						Undo.RecordObject(TargetMeshRenderer, UndoGroupName);
						TargetMeshRenderer.shadowCastingMode = ShadowCastingMode.TwoSided;
						Undo.CollapseUndoOperations(UndoGroupIndex);
					}
					if (TargetMeshRenderer.lightProbeUsage != LightProbeUsage.BlendProbes) {
						Undo.RecordObject(TargetMeshRenderer, UndoGroupName);
						TargetMeshRenderer.lightProbeUsage = LightProbeUsage.BlendProbes;
						Undo.CollapseUndoOperations(UndoGroupIndex);
					}
					if (TargetMeshRenderer.reflectionProbeUsage != ReflectionProbeUsage.Off) {
						Undo.RecordObject(TargetMeshRenderer, UndoGroupName);
						TargetMeshRenderer.reflectionProbeUsage = ReflectionProbeUsage.Off;
						Undo.CollapseUndoOperations(UndoGroupIndex);
					}
					if (TargetMeshRenderer.probeAnchor != AvatarAnchorOverride) {
						Undo.RecordObject(TargetMeshRenderer, UndoGroupName);
						TargetMeshRenderer.probeAnchor = AvatarAnchorOverride;
						Undo.CollapseUndoOperations(UndoGroupIndex);
					}
				}
				Debug.Log($"[VRSuya] Changed {AvatarGameObject.name} Renderer Settings");
			}
		}

		public void RequestUpdateBounds() {
			VRC_AvatarDescriptor AvatarDescriptor = AvatarUtility.GetAvatarDescriptor();
			if (AvatarDescriptor) {
				UndoGroupIndex = UnityUtility.InitializeUndoGroup(UndoGroupName);
				GameObject AvatarGameObject = AvatarDescriptor.gameObject;
				(SkinnedMeshRenderer[] AvatarSkinnedMeshRenderers, MeshRenderer[] AvatarMeshRenderers) = GetAvatarRenderers(AvatarGameObject);
				Bounds NewBounds = new Bounds {
					center = new Vector3(0.0f, 0.0f, 0.0f),
					extents = new Vector3(1.0f, 1.0f, 1.0f),
				};
				foreach (SkinnedMeshRenderer TargetSkinnedMeshRenderer in AvatarSkinnedMeshRenderers) {
					if (TargetSkinnedMeshRenderer.localBounds != NewBounds) {
						Undo.RecordObject(TargetSkinnedMeshRenderer, UndoGroupName);
						TargetSkinnedMeshRenderer.updateWhenOffscreen = true;
						TargetSkinnedMeshRenderer.localBounds = NewBounds;
						TargetSkinnedMeshRenderer.updateWhenOffscreen = false;
						EditorUtility.SetDirty(TargetSkinnedMeshRenderer);
						Undo.CollapseUndoOperations(UndoGroupIndex);
					}
				}
				Debug.Log($"[VRSuya] Changed {AvatarGameObject.name} Bounds");
			}
		}

		public void RequestUpdateTwosidedShadow() {
			VRC_AvatarDescriptor AvatarDescriptor = AvatarUtility.GetAvatarDescriptor();
			if (AvatarDescriptor) {
				UndoGroupIndex = UnityUtility.InitializeUndoGroup(UndoGroupName);
				GameObject AvatarGameObject = AvatarDescriptor.gameObject;
				(SkinnedMeshRenderer[] AvatarSkinnedMeshRenderers, MeshRenderer[] AvatarMeshRenderers) = GetAvatarRenderers(AvatarGameObject);
				foreach (SkinnedMeshRenderer TargetSkinnedMeshRenderer in AvatarSkinnedMeshRenderers) {
					if (TargetSkinnedMeshRenderer.shadowCastingMode != ShadowCastingMode.TwoSided) {
						Undo.RecordObject(TargetSkinnedMeshRenderer, UndoGroupName);
						TargetSkinnedMeshRenderer.shadowCastingMode = ShadowCastingMode.TwoSided;
						Undo.CollapseUndoOperations(UndoGroupIndex);
					}
				}
				foreach (MeshRenderer TargetMeshRenderer in AvatarMeshRenderers) {
					if (TargetMeshRenderer.shadowCastingMode != ShadowCastingMode.TwoSided) {
						Undo.RecordObject(TargetMeshRenderer, UndoGroupName);
						TargetMeshRenderer.shadowCastingMode = ShadowCastingMode.TwoSided;
						Undo.CollapseUndoOperations(UndoGroupIndex);
					}
				}
				Debug.Log($"[VRSuya] Changed {AvatarGameObject.name} Shadow Casting Mode");
			}
		}

		public void RequestUpdateProbes() {
			VRC_AvatarDescriptor AvatarDescriptor = AvatarUtility.GetAvatarDescriptor();
			if (AvatarDescriptor) {
				UndoGroupIndex = UnityUtility.InitializeUndoGroup(UndoGroupName);
				GameObject AvatarGameObject = AvatarDescriptor.gameObject;
				(SkinnedMeshRenderer[] AvatarSkinnedMeshRenderers, MeshRenderer[] AvatarMeshRenderers) = GetAvatarRenderers(AvatarGameObject);
				foreach (SkinnedMeshRenderer TargetSkinnedMeshRenderer in AvatarSkinnedMeshRenderers) {
					if (TargetSkinnedMeshRenderer.lightProbeUsage != LightProbeUsage.BlendProbes) {
						Undo.RecordObject(TargetSkinnedMeshRenderer, UndoGroupName);
						TargetSkinnedMeshRenderer.lightProbeUsage = LightProbeUsage.BlendProbes;
						Undo.CollapseUndoOperations(UndoGroupIndex);
					}
					if (TargetSkinnedMeshRenderer.reflectionProbeUsage != ReflectionProbeUsage.Off) {
						Undo.RecordObject(TargetSkinnedMeshRenderer, UndoGroupName);
						TargetSkinnedMeshRenderer.reflectionProbeUsage = ReflectionProbeUsage.Off;
						Undo.CollapseUndoOperations(UndoGroupIndex);
					}
				}
				foreach (MeshRenderer TargetMeshRenderer in AvatarMeshRenderers) {
					if (TargetMeshRenderer.lightProbeUsage != LightProbeUsage.BlendProbes) {
						Undo.RecordObject(TargetMeshRenderer, UndoGroupName);
						TargetMeshRenderer.lightProbeUsage = LightProbeUsage.BlendProbes;
						Undo.CollapseUndoOperations(UndoGroupIndex);
					}
					if (TargetMeshRenderer.reflectionProbeUsage != ReflectionProbeUsage.Off) {
						Undo.RecordObject(TargetMeshRenderer, UndoGroupName);
						TargetMeshRenderer.reflectionProbeUsage = ReflectionProbeUsage.Off;
						Undo.CollapseUndoOperations(UndoGroupIndex);
					}
				}
				Debug.Log($"[VRSuya] Changed {AvatarGameObject.name} LightProbe Usage Mode");
			}
		}

		public void RequestUpdateAnchorOverride() {
			VRC_AvatarDescriptor AvatarDescriptor = AvatarUtility.GetAvatarDescriptor();
			if (AvatarDescriptor) {
				UndoGroupIndex = UnityUtility.InitializeUndoGroup(UndoGroupName);
				GameObject AvatarGameObject = AvatarDescriptor.gameObject;
				Transform AvatarAnchorOverride = AvatarUtility.GetAvatarAnchorOverride(AvatarGameObject);
				if (!AvatarAnchorOverride) AvatarAnchorOverride = GetAnchorOverride(AvatarGameObject);
				(SkinnedMeshRenderer[] AvatarSkinnedMeshRenderers, MeshRenderer[] AvatarMeshRenderers) = GetAvatarRenderers(AvatarGameObject);
				foreach (SkinnedMeshRenderer TargetSkinnedMeshRenderer in AvatarSkinnedMeshRenderers) {
					if (TargetSkinnedMeshRenderer.probeAnchor != AvatarAnchorOverride) {
						Undo.RecordObject(TargetSkinnedMeshRenderer, UndoGroupName);
						TargetSkinnedMeshRenderer.probeAnchor = AvatarAnchorOverride;
						Undo.CollapseUndoOperations(UndoGroupIndex);
					}
				}
				foreach (MeshRenderer TargetMeshRenderer in AvatarMeshRenderers) {
					if (TargetMeshRenderer.probeAnchor != AvatarAnchorOverride) {
						Undo.RecordObject(TargetMeshRenderer, UndoGroupName);
						TargetMeshRenderer.probeAnchor = AvatarAnchorOverride;
						Undo.CollapseUndoOperations(UndoGroupIndex);
					}
				}
				Debug.Log($"[VRSuya] Changed {AvatarGameObject.name} AnchorOverride");
			}
		}

		(SkinnedMeshRenderer[], MeshRenderer[]) GetAvatarRenderers(GameObject TargetGameObject) {
			return (TargetGameObject.GetComponentsInChildren<SkinnedMeshRenderer>(true), TargetGameObject.GetComponentsInChildren<MeshRenderer>(true));
		}

		Transform GetAnchorOverride(GameObject TargetGameObject) {
			GameObject TargetHeadGameObject = AvatarUtility.GetHeadGameObject(TargetGameObject);
			if (TargetHeadGameObject) {
				Transform TargetHeadTransform = TargetGameObject.transform;
				Transform[] ChildTransforms = TargetHeadTransform.GetComponentsInChildren<Transform>(true);
				if (ChildTransforms.Where(Item => Item.name == "AnchorOverride" && Item.parent == TargetHeadTransform).ToArray().Length > 0) {
					Transform NewAnchorOverride = ChildTransforms.Where(Item => Item.name == "AnchorOverride" && Item.parent == TargetHeadTransform).ToArray()[0];
					return NewAnchorOverride;
				} else {
					GameObject NewChildAnchorOverride = new GameObject("AnchorOverride");
					Undo.RegisterCreatedObjectUndo(NewChildAnchorOverride, UndoGroupName);
					NewChildAnchorOverride.transform.SetParent(TargetHeadTransform, false);
					return NewChildAnchorOverride.transform;
				}
			}
			return null;
		}
	}
}
#endif