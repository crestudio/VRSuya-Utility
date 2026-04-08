#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;

using UnityEditor;
using UnityEngine;

using static VRSuya.Core.Unity;

/*
 * VRSuya Utility
 * Contact : vrsuya@gmail.com // Twitter : https://twitter.com/VRSuya
 */

namespace VRSuya.Utility {

	[ExecuteInEditMode]
	[AddComponentMenu("VRSuya/VRSuya TextureReplacer")]
	public class TextureReplacer : MonoBehaviour {

		[Serializable]
		public struct TextureExpression {
			public bool ShowDetails;
			public Texture2D BeforeTexture;
			public Texture2D AfterTexture;
			public MaterialDetail[] OriginMaterial;

			public TextureExpression(bool ShowDetail, Texture2D ExistTexture, Texture2D NewTexture, MaterialDetail[] ExistMaterials) {
				ShowDetails = ShowDetail;
				BeforeTexture = ExistTexture;
				AfterTexture = NewTexture;
				OriginMaterial = ExistMaterials;
			}
		};

		[Serializable]
		public struct MaterialDetail {
			public Material OriginMaterial;
			public string[] PropertyName;

			public MaterialDetail(Material ExistMaterial, string[] ExsitPropertyName) {
				OriginMaterial = ExistMaterial;
				PropertyName = ExsitPropertyName;
			}
		}

		[SerializeField]
		public List<TextureExpression> AvatarTextures = new List<TextureExpression>();
		List<TextureExpression> TargetTextures = new List<TextureExpression>();

		public GameObject AvatarGameObject = null;
		public Material[] AvatarMaterials = new Material[0];

		const string UndoGroupName = "VRSuya TextureReplacer";
		int UndoGroupIndex;

		void OnEnable() {
			RefreshAvatarProprety();
		}

		public void RequestUpdateAvatarTextures() {
			UndoGroupIndex = InitializeUndoGroup(UndoGroupName);
			TargetTextures = CleanupAvatarTextureList();
			if (AvatarMaterials.Length > 0 && TargetTextures.Count > 0) ChangeTexture2Ds();
		}

		public void RefreshAvatarProprety() {
			if (!AvatarGameObject) {
				AvatarGameObject = this.gameObject;
			}
			AvatarMaterials = GetAvatarMaterials(AvatarGameObject);
			AvatarTextures = GetAvatarTextures(AvatarGameObject);
		}

		List<TextureExpression> GetAvatarTextures(GameObject TargetGameObject) {
			AssetProcessor AssetProcessorInstance = new AssetProcessor();
			TextureExpression[] AvatarTextureExpressions = AssetProcessorInstance.AddAvatarTextureDetails(TargetGameObject);
			List<TextureExpression> newAvatarTextureExpressions = new List<TextureExpression>();
			Texture2D[] ExistTexture = AvatarTextureExpressions.Select(AvatarTexture => AvatarTexture.BeforeTexture).Distinct().ToArray();
			for (int TextureIndex = 0; TextureIndex < ExistTexture.Length; TextureIndex++) {
				MaterialDetail[] TextureMaterials = AvatarTextureExpressions
					.Where(AvatarTexture => AvatarTexture.BeforeTexture == ExistTexture[TextureIndex])
					.SelectMany(AvatarTexture => AvatarTexture.OriginMaterial).ToArray();
				for (int MaterialIndex = 0; MaterialIndex < TextureMaterials.Length; MaterialIndex++) {
					if (newAvatarTextureExpressions.Exists(newAvatarTexture => newAvatarTexture.BeforeTexture == ExistTexture[TextureIndex])) {
						TextureExpression OldAvatarTextureExpression = newAvatarTextureExpressions.Find(newAvatarTexture => newAvatarTexture.BeforeTexture == ExistTexture[TextureIndex]);
						List<MaterialDetail> newMaterialDetail = OldAvatarTextureExpression.OriginMaterial.Concat(new MaterialDetail[] { TextureMaterials[MaterialIndex] }).ToList();
						newMaterialDetail.Sort((a, b) => string.Compare(a.OriginMaterial.name, b.OriginMaterial.name, StringComparison.Ordinal));
						TextureExpression NewAvatarTextureExpression = new TextureExpression() {
							ShowDetails = OldAvatarTextureExpression.ShowDetails,
							BeforeTexture = OldAvatarTextureExpression.BeforeTexture,
							AfterTexture = OldAvatarTextureExpression.AfterTexture,
							OriginMaterial = newMaterialDetail.ToArray()
						};
						
						newAvatarTextureExpressions.Remove(OldAvatarTextureExpression);
						newAvatarTextureExpressions.Add(NewAvatarTextureExpression);
					} else {
						newAvatarTextureExpressions.Add(new TextureExpression(false, ExistTexture[TextureIndex], ExistTexture[TextureIndex], new MaterialDetail[] { TextureMaterials[MaterialIndex] }));
					}
				}
			}
			newAvatarTextureExpressions.Sort((a, b) => string.Compare(a.BeforeTexture.name, b.BeforeTexture.name, StringComparison.Ordinal));
			return newAvatarTextureExpressions;
		}

		Material[] GetAvatarMaterials(GameObject TargetGameObject) {
			AssetProcessor AssetProcessorInstance = new AssetProcessor();
			Material[] newAvatarMaterials = AssetProcessorInstance.GetAvatarMaterials(TargetGameObject);
			return newAvatarMaterials;
		}

		List<TextureExpression> CleanupAvatarTextureList() {
			List<TextureExpression> newTargetTextureList = new List<TextureExpression>();
			foreach (TextureExpression TargetExpression in AvatarTextures) {
				if (TargetExpression.BeforeTexture != TargetExpression.AfterTexture) {
					newTargetTextureList.Add(TargetExpression);
				}
			}
			return newTargetTextureList;
		}

		void ChangeTexture2Ds() {
			int ChangedCount = 0;
			Texture2D[] TargetTexture2Ds = TargetTextures.Select(TargetTexture => TargetTexture.BeforeTexture).ToArray();
			foreach (Material TargetMaterial in AvatarMaterials) {
				if (TargetMaterial) {
					Shader TargetShader = TargetMaterial.shader;
					int PropertyCount = ShaderUtil.GetPropertyCount(TargetShader);
					for (int Index = 0; Index < PropertyCount; Index++) {
						if (ShaderUtil.GetPropertyType(TargetShader, Index) == ShaderUtil.ShaderPropertyType.TexEnv) {
							string PropertyName = ShaderUtil.GetPropertyName(TargetShader, Index);
							Texture ExistMaterialTexture = TargetMaterial.GetTexture(PropertyName);
							if (ExistMaterialTexture is Texture2D) {
								if (Array.Exists(TargetTexture2Ds, TargetTexture => ExistMaterialTexture == TargetTexture)) {
									Undo.RecordObject(TargetMaterial, UndoGroupName);
									Texture2D newTexture2D = TargetTextures
										.Where(TargetTextureExpression => ExistMaterialTexture == TargetTextureExpression.BeforeTexture)
										.Select(TargetTextureExpression => TargetTextureExpression.AfterTexture).ToArray()[0];
									TargetMaterial.SetTexture(PropertyName, newTexture2D);
									EditorUtility.SetDirty(TargetMaterial);
									Undo.CollapseUndoOperations(UndoGroupIndex);
									ChangedCount++;
								}
							}
						}
					}
				}
			}
			Debug.Log($"[VRSuya] {ChangedCount} textures have been replaced");
		}
	}
}
#endif