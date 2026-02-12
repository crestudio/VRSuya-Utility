#if UNITY_EDITOR
using System.Linq;

using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

using VRC.SDKBase;

using static VRSuya.Core.Unity;

/*
 * VRSuya Utility
 * Contact : vrsuya@gmail.com // Twitter : https://twitter.com/VRSuya
 */

namespace com.vrsuya.utility {

	public class MeshRendererController : ScriptableObject {

		readonly string UndoGroupName = "VRSuya MeshRendererController";
		int UndoGroupIndex;

		public void RequestUpdateAvatarRenders() {
			VRSuya.Core.Avatar AvatarInstance = new VRSuya.Core.Avatar();
			VRC_AvatarDescriptor AvatarDescriptor = AvatarInstance.GetVRCAvatarDescriptor();
			if (AvatarDescriptor) {
				UndoGroupIndex = InitializeUndoGroup(UndoGroupName);
				GameObject AvatarGameObject = AvatarDescriptor.gameObject;
				Transform AvatarAnchorOverride = GetAnchorOverride(AvatarGameObject);
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
			VRSuya.Core.Avatar AvatarInstance = new VRSuya.Core.Avatar();
			VRC_AvatarDescriptor AvatarDescriptor = AvatarInstance.GetVRCAvatarDescriptor();
			if (AvatarDescriptor) {
				UndoGroupIndex = InitializeUndoGroup(UndoGroupName);
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
			VRSuya.Core.Avatar AvatarInstance = new VRSuya.Core.Avatar();
			VRC_AvatarDescriptor AvatarDescriptor = AvatarInstance.GetVRCAvatarDescriptor();
			if (AvatarDescriptor) {
				UndoGroupIndex = InitializeUndoGroup(UndoGroupName);
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
				Debug.Log($"[VRSuya] Changed {AvatarGameObject.name} AnchorOverride");
			}
		}

		public void RequestUpdateProbes() {
			VRSuya.Core.Avatar AvatarInstance = new VRSuya.Core.Avatar();
			VRC_AvatarDescriptor AvatarDescriptor = AvatarInstance.GetVRCAvatarDescriptor();
			if (AvatarDescriptor) {
				UndoGroupIndex = InitializeUndoGroup(UndoGroupName);
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
				Debug.Log($"[VRSuya] Changed {AvatarGameObject.name} AnchorOverride");
			}
		}

		public void RequestUpdateAnchorOverride() {
			VRSuya.Core.Avatar AvatarInstance = new VRSuya.Core.Avatar();
			VRC_AvatarDescriptor AvatarDescriptor = AvatarInstance.GetVRCAvatarDescriptor();
			if (AvatarDescriptor) {
				UndoGroupIndex = InitializeUndoGroup(UndoGroupName);
				GameObject AvatarGameObject = AvatarDescriptor.gameObject;
				Transform AvatarAnchorOverride = GetAnchorOverride(AvatarGameObject);
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
			Animator TargetAnimator = TargetGameObject.GetComponent<Animator>();
			if (TargetAnimator) {
				Transform TargetHeadTransform = TargetAnimator.GetBoneTransform(HumanBodyBones.Head);
				if (TargetHeadTransform) {
					Transform[] ChildTransforms = TargetHeadTransform.GetComponentsInChildren<Transform>(true);
					if (ChildTransforms.Where(Item => Item.name == "AnchorOverride" && Item.parent == TargetHeadTransform).ToArray().Length > 0) {
						Transform NewAnchorOverride = ChildTransforms.Where(Item => Item.name == "AnchorOverride" && Item.parent == TargetHeadTransform).ToArray()[0];
						return NewAnchorOverride;
					} else {
						GameObject NewChildAnchorOverride = new GameObject("AnchorOverride");
						Undo.RegisterCreatedObjectUndo(NewChildAnchorOverride, UndoGroupName);
						NewChildAnchorOverride.transform.SetParent(TargetHeadTransform);
						NewChildAnchorOverride.transform.localPosition = Vector3.zero;
						NewChildAnchorOverride.transform.localRotation = Quaternion.identity;
						NewChildAnchorOverride.transform.localScale = Vector3.one;
						return NewChildAnchorOverride.transform;
					}
				} else {
					return null;
				}
			} else {
				return null;
			}
		}
	}
}
#endif