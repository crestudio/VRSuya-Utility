#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;

using UnityEditor;
using UnityEngine;

/*
 * VRSuya Utility
 * Contact : vrsuya@gmail.com // Twitter : https://twitter.com/VRSuya
 */

namespace com.vrsuya.utility {

	public class MaterialTemplate : ScriptableObject {

		public GameObject TargetGameObject = null;
		public Material ReferenceMaterial = null;
		public Material[] TargetMaterials = new Material[0];
		public Texture2D[] TargetTexture2Ds = new Texture2D[0];

		// 쉐이더 별 업데이트 여부
		public bool UpdatelilToon = true;
		public bool Updatepoiyomi = true;
		public bool UpdateUnityChanToonShader = true;

		// 릴툰 프로퍼티 업데이트 설정
		public bool UpdatelilToonBasic = true;
		public bool UpdatelilToonLighting = true;
		public bool UpdatelilToonShadow = true;
		public bool UpdatelilToonReceiveShadow = true;
		public bool UpdatelilToonBackfaceMask = true;
		public bool UpdatelilToonBacklight = true;

		// 포이요미 프로퍼티 업데이트 설정

		// UTS 프로퍼티 업데이트 설정
		public bool UpdateUTSTextureShared = true;
		public bool UpdateUTSNormalMap = true;
		public bool UpdateUTSBasicShading = true;
		public bool UpdateUTSLightColor = true;
		public bool UpdateUTSEnvironmentalLightingPropertys = true;

		// 공용 프로퍼티 업데이트 설정
		public bool UpdateRenderQueue = true;
		public bool UpdateGPUInstancing = true;
		public bool UpdateGlobalIllumination = true;

		// 텍스쳐 프로퍼티 업데이트 설정
		public bool AnalyzeTextures = false;
		public bool UpdatesRGB = true;
		public bool UpdateNormal = true;
		public bool UpdateAlpha = true;
		public bool UpdateMaxTextureSize = true;
		public bool UpdateOverrideStandalone = true;

		public string ReturnString;

		public enum ShaderType {
			lilToon,
			poiyomi,
			UnityChanToonShader
		}

		/// <summary>
		/// 본 프로그램의 메인 세팅 로직입니다.
		/// </summary>
		public void UpdateMaterialPropertys() {
			foreach (Material TargetMaterial in TargetMaterials) {
				if (TargetMaterial) {
					string ShaderType = GetShaderType(TargetMaterial);
					switch (ShaderType) {
						case "lilToon":
							UpdatelilToonPropertys(TargetMaterial);
							break;
						case "poiyomi":
							UpdatepoiyomiPropertys(TargetMaterial);
							break;
						case "UnityChanToonShader":
							UpdateUnityChanToonShaderPropertys(TargetMaterial);
							break;
						default:
							Debug.LogError($"[VRSuya] {ShaderType} 쉐이더는 지원하지 않습니다!");
							break;
					}
				}
			}
			foreach (Texture2D TargetTexture2D in TargetTexture2Ds) {
				if (TargetTexture2D) {
					UpdateTexture2DSharedPropertys(TargetTexture2D);
				}
			}
			Debug.Log($"[VRSuya] 머테리얼 일괄 변경 처리가 완료 되었습니다!");
		}

		/// <summary>타켓 머테리얼 목록에 lilToon 쉐이더 머테리얼 전수를 삽입합니다.</summary>
		public void AddlilToonMaterials() {
			TargetMaterials = TargetMaterials.Concat(GetRequestMaterials(ShaderType.lilToon)).ToArray();
		}

		/// <summary>타켓 머테리얼 목록에 poiyomi 쉐이더 머테리얼 전수를 삽입합니다.</summary>
		public void AddpoiyomiMaterials() {
			TargetMaterials = TargetMaterials.Concat(GetRequestMaterials(ShaderType.poiyomi)).ToArray();
		}

		/// <summary>타켓 머테리얼 목록에 UnityChanToonShader 쉐이더 머테리얼 전수를 삽입합니다.</summary>
		public void AddUnityChanToonShaderMaterials() {
			TargetMaterials = TargetMaterials.Concat(GetRequestMaterials(ShaderType.UnityChanToonShader)).ToArray();
		}

		/// <summary>해당 머테리얼이 어떠한 쉐이더를 사용하는지 String으로 반환합니다.</summary>
		/// <returns>머테리얼이 사용하고 있는 쉐이더</returns>
		string GetShaderType(Material TargetMaterial) {
			string ShaderType = TargetMaterial.shader.name;
			if (TargetMaterial.shader.name.Contains("lilToon")) ShaderType = "lilToon";
			if (TargetMaterial.shader.name.Contains("poiyomi")) ShaderType = "poiyomi";
			if (TargetMaterial.shader.name.Contains("UnityChanToonShader")) ShaderType = "UnityChanToonShader";
			return ShaderType;
		}

		/// <summary>릴툰 머테리얼의 메인 일괄처리 프로세스 입니다.</summary>
		void UpdatelilToonPropertys(Material TargetMaterial) {
			if (UpdatelilToonBasic) UpdatelilToonBasicPropertys(TargetMaterial);
			if (UpdatelilToonLighting) UpdatelilToonLightingPropertys(TargetMaterial);
			if (UpdatelilToonShadow) UpdatelilToonShadowPropertys(TargetMaterial);
			if (UpdatelilToonReceiveShadow) UpdatelilToonReceiveShadowPropertys(TargetMaterial);
			if (UpdatelilToonBackfaceMask) UpdatelilToonBackfaceMaskPropertys(TargetMaterial);
			if (UpdatelilToonBacklight) UpdatelilToonBacklightPropertys(TargetMaterial);
			if (UpdateRenderQueue) UpdateRenderQueuePropertys(TargetMaterial);
			if (UpdateGPUInstancing) UpdateGPUInstancingPropertys(TargetMaterial);
			if (UpdateGlobalIllumination) UpdateGlobalIlluminationPropertys(TargetMaterial);
		}

		/// <summary>poiyomi 머테리얼의 메인 일괄처리 프로세스 입니다.</summary>
		void UpdatepoiyomiPropertys(Material TargetMaterial) {
			if (UpdateRenderQueue) UpdateRenderQueuePropertys(TargetMaterial);
			if (UpdateGPUInstancing) UpdateGPUInstancingPropertys(TargetMaterial);
			if (UpdateGlobalIllumination) UpdateGlobalIlluminationPropertys(TargetMaterial);
		}

		/// <summary>UnityChanToonShader 머테리얼의 메인 일괄처리 프로세스 입니다.</summary>
		void UpdateUnityChanToonShaderPropertys(Material TargetMaterial) {
			if (UpdateUTSTextureShared) UpdateTextureSharedPropertys(TargetMaterial);
			if (UpdateUTSNormalMap) UpdateNormalMapPropertys(TargetMaterial);
			if (UpdateUTSBasicShading) UpdateBasicShadingPropertys(TargetMaterial);
			if (UpdateUTSLightColor) UpdateLightColorPropertys(TargetMaterial);
			if (UpdateUTSEnvironmentalLightingPropertys) UpdateEnvironmentalLightingPropertys(TargetMaterial);
			if (UpdateRenderQueue) UpdateRenderQueuePropertys(TargetMaterial);
			if (UpdateGPUInstancing) UpdateGPUInstancingPropertys(TargetMaterial);
			if (UpdateGlobalIllumination) UpdateGlobalIlluminationPropertys(TargetMaterial);
		}

		// lilToon 프로퍼티

		/// <summary>해당 릴툰 머테리얼에서 Basic 프로퍼티 값을 일괄 변경합니다.</summary>
		void UpdatelilToonBasicPropertys(Material TargetMaterial) {
			bool IsDrity = false;
			float Cutoff = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Cutoff") : 0.5f;
			float Cull = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Cull") : 2.0f;
			float FlipNormal = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_FlipNormal") : 1.0f;
			float BackfaceForceShadow = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_BackfaceForceShadow") : 1.0f;
			float AlphaMaskValue = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_AlphaMaskValue") : 0.0f;
			if (TargetMaterial.GetFloat("_Cutoff") != Cutoff) { TargetMaterial.SetFloat("_Cutoff", Cutoff); IsDrity = true; }
			if (TargetMaterial.GetFloat("_Cull") != Cull) { TargetMaterial.SetFloat("_Cull", Cull); IsDrity = true; }
			if (TargetMaterial.GetFloat("_FlipNormal") != FlipNormal) { TargetMaterial.SetFloat("_FlipNormal", FlipNormal); IsDrity = true; }
			if (TargetMaterial.GetFloat("_BackfaceForceShadow") != BackfaceForceShadow) { TargetMaterial.SetFloat("_BackfaceForceShadow", BackfaceForceShadow); IsDrity = true; }
			if (TargetMaterial.GetFloat("_AlphaMaskValue") != AlphaMaskValue) { TargetMaterial.SetFloat("_AlphaMaskValue", AlphaMaskValue); IsDrity = true; }
			if (IsDrity) {
				EditorUtility.SetDirty(TargetMaterial);
				Debug.Log($"[VRSuya] {TargetMaterial.name} 머테리얼 릴툰 쉐이더의 Basic 프로퍼티가 변경되었습니다");
			}
		}

		/// <summary>해당 릴툰 머테리얼에서 Lighting 프로퍼티 값을 일괄 변경합니다.</summary>
		void UpdatelilToonLightingPropertys(Material TargetMaterial) {
			bool IsDrity = false;
			float LightMinLimit = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_LightMinLimit") : 0.0f;
			float LightMaxLimit = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_LightMaxLimit") : 1.0f;
			float MonochromeLighting = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_MonochromeLighting") : 0.0f;
			float ShadowEnvStrength = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_ShadowEnvStrength") : 1.0f;
			float AsUnlit = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_AsUnlit") : 0.0f;
			float VertexLightStrength = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_VertexLightStrength") : 0.0f;
			Color LightDirectionOverride = (ReferenceMaterial) ? ReferenceMaterial.GetColor("_LightDirectionOverride") : new Color(0.0f, 0.001f, 0.0f, 0.0f);
			if (TargetMaterial.GetFloat("_LightMinLimit") != LightMinLimit) { TargetMaterial.SetFloat("_LightMinLimit", LightMinLimit); IsDrity = true; }
			if (TargetMaterial.GetFloat("_LightMaxLimit") != LightMaxLimit) { TargetMaterial.SetFloat("_LightMaxLimit", LightMaxLimit); IsDrity = true; }
			if (TargetMaterial.GetFloat("_MonochromeLighting") != MonochromeLighting) { TargetMaterial.SetFloat("_MonochromeLighting", MonochromeLighting); IsDrity = true; }
			if (TargetMaterial.GetFloat("_ShadowEnvStrength") != ShadowEnvStrength) { TargetMaterial.SetFloat("_ShadowEnvStrength", ShadowEnvStrength); IsDrity = true; }
			if (TargetMaterial.GetFloat("_AsUnlit") != AsUnlit) { TargetMaterial.SetFloat("_AsUnlit", AsUnlit); IsDrity = true; }
			if (TargetMaterial.GetFloat("_VertexLightStrength") != VertexLightStrength) { TargetMaterial.SetFloat("_VertexLightStrength", VertexLightStrength); IsDrity = true; }
			if (TargetMaterial.GetColor("_LightDirectionOverride") != LightDirectionOverride) { TargetMaterial.SetColor("_LightDirectionOverride", LightDirectionOverride); IsDrity = true; }
			if (IsDrity) {
				EditorUtility.SetDirty(TargetMaterial);
				Debug.Log($"[VRSuya] {TargetMaterial.name} 머테리얼 릴툰 쉐이더의 라이팅 프로퍼티가 변경되었습니다");
			}
		}

		/// <summary>해당 릴툰 머테리얼에서 Shadow 프로퍼티 값을 일괄 변경합니다.</summary>
		void UpdatelilToonShadowPropertys(Material TargetMaterial) {
			bool IsDrity = false;
			float ShadowBorder = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_ShadowBorder") : 0.6f;
			float ShadowBlur = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_ShadowBlur") : 0.15f;
			float ShadowNormalStrength = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_ShadowNormalStrength") : 1.0f;
			float Shadow2ndBorder = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Shadow2ndBorder") : 0.4f;
			float Shadow2ndBlur = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Shadow2ndBlur") : 0.15f;
			float Shadow2ndNormalStrength = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Shadow2ndNormalStrength") : 1.0f;
			float Shadow3rdBorder = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Shadow3rdBorder") : 0.2f;
			float Shadow3rdBlur = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Shadow3rdBlur") : 0.15f;
			float Shadow3rdNormalStrength = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Shadow3rdNormalStrength") : 1.0f;
			float ShadowBorderRange = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_ShadowBorderRange") : 0.0f;
			float ShadowMainStrength = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_ShadowMainStrength") : 0.0f;
			float ShadowEnvStrength = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_ShadowEnvStrength") : 1.0f;
			if (TargetMaterial.name.Contains("Head")) {
				ShadowBorder = 0.25f;
				Shadow2ndBorder = 0.15f;
				Shadow3rdBorder = 0.05f;
			}
			if (TargetMaterial.GetFloat("_ShadowBorder") != ShadowBorder) { TargetMaterial.SetFloat("_ShadowBorder", ShadowBorder); IsDrity = true; }
			if (TargetMaterial.GetFloat("_ShadowBlur") != ShadowBlur) { TargetMaterial.SetFloat("_ShadowBlur", ShadowBlur); IsDrity = true; }
			if (TargetMaterial.GetFloat("_ShadowNormalStrength") != ShadowNormalStrength) { TargetMaterial.SetFloat("_ShadowNormalStrength", ShadowNormalStrength); IsDrity = true; }
			if (TargetMaterial.GetFloat("_Shadow2ndBorder") != Shadow2ndBorder) { TargetMaterial.SetFloat("_Shadow2ndBorder", Shadow2ndBorder); IsDrity = true; }
			if (TargetMaterial.GetFloat("_Shadow2ndBlur") != Shadow2ndBlur) { TargetMaterial.SetFloat("_Shadow2ndBlur", Shadow2ndBlur); IsDrity = true; }
			if (TargetMaterial.GetFloat("_Shadow2ndNormalStrength") != Shadow2ndNormalStrength) { TargetMaterial.SetFloat("_Shadow2ndNormalStrength", Shadow2ndNormalStrength); IsDrity = true; }
			if (TargetMaterial.GetFloat("_Shadow3rdBorder") != Shadow3rdBorder) { TargetMaterial.SetFloat("_Shadow3rdBorder", Shadow3rdBorder); IsDrity = true; }
			if (TargetMaterial.GetFloat("_Shadow3rdBlur") != Shadow3rdBlur) { TargetMaterial.SetFloat("_Shadow3rdBlur", Shadow3rdBlur); IsDrity = true; }
			if (TargetMaterial.GetFloat("_Shadow3rdNormalStrength") != Shadow3rdNormalStrength) { TargetMaterial.SetFloat("_Shadow3rdNormalStrength", Shadow3rdNormalStrength); IsDrity = true; }
			if (TargetMaterial.GetFloat("_ShadowBorderRange") != ShadowBorderRange) { TargetMaterial.SetFloat("_ShadowBorderRange", ShadowBorderRange); IsDrity = true; }
			if (TargetMaterial.GetFloat("_ShadowMainStrength") != ShadowMainStrength) { TargetMaterial.SetFloat("_ShadowMainStrength", ShadowMainStrength); IsDrity = true; }
			if (TargetMaterial.GetFloat("_ShadowEnvStrength") != ShadowEnvStrength) { TargetMaterial.SetFloat("_ShadowEnvStrength", ShadowEnvStrength); IsDrity = true; }
			if (IsDrity) {
				EditorUtility.SetDirty(TargetMaterial);
				Debug.Log($"[VRSuya] {TargetMaterial.name} 머테리얼 릴툰 쉐이더의 그림자 프로퍼티가 변경되었습니다");
			}
		}

		/// <summary>해당 릴툰 머테리얼에서 Receive Shadow 프로퍼티 값을 일괄 변경합니다.</summary>
		void UpdatelilToonReceiveShadowPropertys(Material TargetMaterial) {
			bool IsDrity = false;
			float ShadowReceive = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_ShadowReceive") : 1.0f;
			float Shadow2ndReceive = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Shadow2ndReceive") : 1.0f;
			float Shadow3rdReceive = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Shadow3rdReceive") : 1.0f;
			if (TargetMaterial.GetFloat("_ShadowReceive") != ShadowReceive) { TargetMaterial.SetFloat("_ShadowReceive", ShadowReceive); IsDrity = true; }
			if (TargetMaterial.GetFloat("_Shadow2ndReceive") != Shadow2ndReceive) { TargetMaterial.SetFloat("_Shadow2ndReceive", Shadow2ndReceive); IsDrity = true; }
			if (TargetMaterial.GetFloat("_Shadow3rdReceive") != Shadow3rdReceive) { TargetMaterial.SetFloat("_Shadow3rdReceive", Shadow3rdReceive); IsDrity = true; }
			if (IsDrity) {
				EditorUtility.SetDirty(TargetMaterial);
				Debug.Log($"[VRSuya] {TargetMaterial.name} 머테리얼 릴툰 쉐이더의 그림자 영향 프로퍼티가 변경되었습니다");
			}
		}

		/// <summary>해당 릴툰 머테리얼에서 BackfaceMask 프로퍼티 값을 일괄 변경합니다.</summary>
		void UpdatelilToonBackfaceMaskPropertys(Material TargetMaterial) {
			bool IsDrity = false;
			float BackfaceMask = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_MatCapBackfaceMask") : 1.0f;
			if (TargetMaterial.GetFloat("_BacklightBackfaceMask") != BackfaceMask) { TargetMaterial.SetFloat("_BacklightBackfaceMask", BackfaceMask); IsDrity = true; }
			if (TargetMaterial.GetFloat("_GlitterBackfaceMask") != BackfaceMask) { TargetMaterial.SetFloat("_GlitterBackfaceMask", BackfaceMask); IsDrity = true; }
			if (TargetMaterial.GetFloat("_MatCap2ndBackfaceMask") != BackfaceMask) { TargetMaterial.SetFloat("_MatCap2ndBackfaceMask", BackfaceMask); IsDrity = true; }
			if (TargetMaterial.GetFloat("_MatCapBackfaceMask") != BackfaceMask) { TargetMaterial.SetFloat("_MatCapBackfaceMask", BackfaceMask); IsDrity = true; }
			if (TargetMaterial.GetFloat("_RimBackfaceMask") != BackfaceMask) { TargetMaterial.SetFloat("_RimBackfaceMask", BackfaceMask); IsDrity = true; }
			if (IsDrity) {
				EditorUtility.SetDirty(TargetMaterial);
				Debug.Log($"[VRSuya] {TargetMaterial.name} 머테리얼 릴툰 쉐이더의 BackfaceMask 프로퍼티가 변경되었습니다");
			}
		}

		/// <summary>해당 릴툰 머테리얼에서 Backlight 프로퍼티 값을 일괄 변경합니다.</summary>
		void UpdatelilToonBacklightPropertys(Material TargetMaterial) {
			bool IsDrity = false;
			float BacklightMainStrength = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_BacklightMainStrength") : 0.3f;
			float BacklightBorder = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_BacklightBorder") : 0.8f;
			float BacklightBlur = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_BacklightBlur") : 0.3f;
			float BacklightDirectivity = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_BacklightDirectivity") : 2.0f;
			if (TargetMaterial.GetFloat("_BacklightMainStrength") != BacklightMainStrength) { TargetMaterial.SetFloat("_BacklightMainStrength", BacklightMainStrength); IsDrity = true; }
			if (TargetMaterial.GetFloat("_BacklightBorder") != BacklightBorder) { TargetMaterial.SetFloat("_BacklightBorder", BacklightBorder); IsDrity = true; }
			if (TargetMaterial.GetFloat("_BacklightBlur") != BacklightBlur) { TargetMaterial.SetFloat("_BacklightBlur", BacklightBlur); IsDrity = true; }
			if (TargetMaterial.GetFloat("_BacklightDirectivity") != BacklightDirectivity) { TargetMaterial.SetFloat("_BacklightDirectivity", BacklightDirectivity); IsDrity = true; }
			if (IsDrity) {
				EditorUtility.SetDirty(TargetMaterial);
				Debug.Log($"[VRSuya] {TargetMaterial.name} 머테리얼 릴툰 쉐이더의 Backlight 프로퍼티가 변경되었습니다");
			}
		}

		// UnityChanToonShader 프로퍼티

		/// <summary>해당 UTS 머테리얼에서 텍스쳐 공유 프로퍼티 값을 일괄 변경합니다.</summary>
		void UpdateTextureSharedPropertys(Material TargetMaterial) {
			bool IsDrity = false;
			float TextureShared = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Use_BaseAs1st") : 1.0f;
			if (TargetMaterial.GetFloat("_Use_BaseAs1st") != TextureShared) { TargetMaterial.SetFloat("_Use_BaseAs1st", TextureShared); IsDrity = true; }
			if (TargetMaterial.GetTexture("_1st_ShadeMap") != null) { TargetMaterial.SetTexture("_1st_ShadeMap", null); IsDrity = true; }
			if (TargetMaterial.GetFloat("_Use_1stAs2nd") != TextureShared) { TargetMaterial.SetFloat("_Use_1stAs2nd", TextureShared); IsDrity = true; }
			if (TargetMaterial.GetTexture("_2nd_ShadeMap") != null) { TargetMaterial.SetTexture("_2nd_ShadeMap", null); IsDrity = true; }
			if (IsDrity) {
				EditorUtility.SetDirty(TargetMaterial);
				Debug.Log($"[VRSuya] {TargetMaterial.name} 머테리얼 UTS 쉐이더의 텍스쳐 공유 프로퍼티가 변경되었습니다");
			}
		}

		/// <summary>해당 UTS 머테리얼에서 노멀맵 프로퍼티 값을 일괄 변경합니다.</summary>
		void UpdateNormalMapPropertys(Material TargetMaterial) {
			bool IsDrity = false;
			float Is_NormalMapToBase = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Is_NormalMapToBase") : 1.0f;
			float Is_NormalMapToHighColor = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Is_NormalMapToHighColor") : 1.0f;
			float Is_NormalMapToRimLight = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Is_NormalMapToRimLight") : 1.0f;
			if (TargetMaterial.GetFloat("_Is_NormalMapToBase") != Is_NormalMapToBase) { TargetMaterial.SetFloat("_Is_NormalMapToBase", Is_NormalMapToBase); IsDrity = true; }
			if (TargetMaterial.GetFloat("_Is_NormalMapToHighColor") != Is_NormalMapToHighColor) { TargetMaterial.SetFloat("_Is_NormalMapToHighColor", Is_NormalMapToHighColor); IsDrity = true; }
			if (TargetMaterial.GetFloat("_Is_NormalMapToRimLight") != Is_NormalMapToRimLight) { TargetMaterial.SetFloat("_Is_NormalMapToRimLight", Is_NormalMapToRimLight); IsDrity = true; }
			if (IsDrity) {
				EditorUtility.SetDirty(TargetMaterial);
				Debug.Log($"[VRSuya] {TargetMaterial.name} 머테리얼 UTS 쉐이더의 노멀맵 프로퍼티가 변경되었습니다");
			}
		}

		/// <summary>해당 UTS 머테리얼에서 기본 쉐이딩 프로퍼티 값을 일괄 변경합니다.</summary>
		void UpdateBasicShadingPropertys(Material TargetMaterial) {
			bool IsDrity = false;
			float Set_SystemShadowsToBase = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Set_SystemShadowsToBase") : 0.0f;
			float Is_Filter_HiCutPointLightColor = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Is_Filter_HiCutPointLightColor") : 0.0f;
			if (TargetMaterial.GetFloat("_Set_SystemShadowsToBase") != Set_SystemShadowsToBase) { TargetMaterial.SetFloat("_Set_SystemShadowsToBase", Set_SystemShadowsToBase); IsDrity = true; }
			if (TargetMaterial.GetFloat("_Is_Filter_HiCutPointLightColor") != Is_Filter_HiCutPointLightColor) { TargetMaterial.SetFloat("_Is_Filter_HiCutPointLightColor", Is_Filter_HiCutPointLightColor); IsDrity = true; }
			if (IsDrity) {
				EditorUtility.SetDirty(TargetMaterial);
				Debug.Log($"[VRSuya] {TargetMaterial.name} 머테리얼 UTS 쉐이더의 기본 쉐이딩 프로퍼티가 변경되었습니다");
			}
		}

		/// <summary>해당 UTS 머테리얼에서 주광색 프로퍼티 값을 일괄 변경합니다.</summary>
		void UpdateLightColorPropertys(Material TargetMaterial) {
			bool IsDrity = false;
			float Is_LightColor_1st_Shade = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Is_LightColor_1st_Shade") : 1.0f;
			float Is_LightColor_2nd_Shade = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Is_LightColor_2nd_Shade") : 1.0f;
			float Is_LightColor_Ap_RimLight = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Is_LightColor_Ap_RimLight") : 1.0f;
			float Is_LightColor_Base = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Is_LightColor_Base") : 1.0f;
			float Is_LightColor_HighColor = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Is_LightColor_HighColor") : 1.0f;
			float Is_LightColor_MatCap = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Is_LightColor_MatCap") : 1.0f;
			float Is_LightColor_Outline = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Is_LightColor_Outline") : 1.0f;
			float Is_LightColor_RimLight = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Is_LightColor_RimLight") : 1.0f;
			if (TargetMaterial.GetFloat("_Is_LightColor_1st_Shade") != Is_LightColor_1st_Shade) { TargetMaterial.SetFloat("_Is_LightColor_1st_Shade", Is_LightColor_1st_Shade); IsDrity = true; }
			if (TargetMaterial.GetFloat("_Is_LightColor_2nd_Shade") != Is_LightColor_2nd_Shade) { TargetMaterial.SetFloat("_Is_LightColor_2nd_Shade", Is_LightColor_2nd_Shade); IsDrity = true; }
			if (TargetMaterial.GetFloat("_Is_LightColor_Ap_RimLight") != Is_LightColor_Ap_RimLight) { TargetMaterial.SetFloat("_Is_LightColor_Ap_RimLight", Is_LightColor_Ap_RimLight); IsDrity = true; }
			if (TargetMaterial.GetFloat("_Is_LightColor_Base") != Is_LightColor_Base) { TargetMaterial.SetFloat("_Is_LightColor_Base", Is_LightColor_Base); IsDrity = true; }
			if (TargetMaterial.GetFloat("_Is_LightColor_HighColor") != Is_LightColor_HighColor) { TargetMaterial.SetFloat("_Is_LightColor_HighColor", Is_LightColor_HighColor); IsDrity = true; }
			if (TargetMaterial.GetFloat("_Is_LightColor_MatCap") != Is_LightColor_MatCap) { TargetMaterial.SetFloat("_Is_LightColor_MatCap", Is_LightColor_MatCap); IsDrity = true; }
			if (TargetMaterial.GetFloat("_Is_LightColor_RimLight") != Is_LightColor_RimLight) { TargetMaterial.SetFloat("_Is_LightColor_RimLight", Is_LightColor_RimLight); IsDrity = true; }
			if (!TargetMaterial.shader.name.Contains("NoOutline")) {
				if (TargetMaterial.GetFloat("_Is_LightColor_Outline") != Is_LightColor_Outline) { TargetMaterial.SetFloat("_Is_LightColor_Outline", Is_LightColor_Outline); IsDrity = true; }
			}
			if (IsDrity) {
				EditorUtility.SetDirty(TargetMaterial);
				Debug.Log($"[VRSuya] {TargetMaterial.name} 머테리얼 UTS 쉐이더의 주광색 프로퍼티가 변경되었습니다");
			}
		}

		/// <summary>해당 UTS 머테리얼에서 환경광 프로퍼티 값을 일괄 변경합니다.</summary>
		void UpdateEnvironmentalLightingPropertys(Material TargetMaterial) {
			bool IsDrity = false;
			float GI_Intensity = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_GI_Intensity") : 0.0f;
			float Unlit_Intensity = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Unlit_Intensity") : 1.0f;
			float Is_Filter_LightColor = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Is_Filter_LightColor") : 0.0f;
			float Is_BLD = (ReferenceMaterial) ? ReferenceMaterial.GetFloat("_Is_BLD") : 0.0f;
			if (TargetMaterial.GetFloat("_GI_Intensity") != GI_Intensity) { TargetMaterial.SetFloat("_GI_Intensity", GI_Intensity); IsDrity = true; }
			if (TargetMaterial.GetFloat("_Unlit_Intensity") != Unlit_Intensity) { TargetMaterial.SetFloat("_Unlit_Intensity", Unlit_Intensity); IsDrity = true; }
			if (TargetMaterial.GetFloat("_Is_Filter_LightColor") != Is_Filter_LightColor) { TargetMaterial.SetFloat("_Is_Filter_LightColor", Is_Filter_LightColor); IsDrity = true; }
			if (TargetMaterial.GetFloat("_Is_BLD") != Is_BLD) { TargetMaterial.SetFloat("_Is_BLD", Is_BLD); IsDrity = true; }
			if (IsDrity) {
				EditorUtility.SetDirty(TargetMaterial);
				Debug.Log($"[VRSuya] {TargetMaterial.name} 머테리얼 UTS 쉐이더의 환경광 프로퍼티가 변경되었습니다");
			}
		}

		// 공용 프로퍼티

		/// <summary>해당 머테리얼에서 RenderQueue 프로퍼티 값을 일괄 변경합니다.</summary>
		void UpdateRenderQueuePropertys(Material TargetMaterial) {
			bool IsDrity = false;
			bool IsTransparent = TargetMaterial.shader.name.Contains("Transparent");
			int RenderQueue = (!IsTransparent) ? -1 : 3000;
			if (TargetMaterial.renderQueue != RenderQueue) { TargetMaterial.renderQueue = RenderQueue; IsDrity = true; }
			if (IsDrity) {
				EditorUtility.SetDirty(TargetMaterial);
				Debug.Log($"[VRSuya] {TargetMaterial.name} 머테리얼의 RenderQueue 프로퍼티가 변경되었습니다");
			}
		}

		/// <summary>해당 머테리얼에서 GPU Instancing 프로퍼티 값을 일괄 변경합니다.</summary>
		void UpdateGPUInstancingPropertys(Material TargetMaterial) {
			bool IsDrity = false;
			bool EnableInstancingVariants = true;
			if (TargetMaterial.enableInstancing != EnableInstancingVariants) { TargetMaterial.enableInstancing = EnableInstancingVariants; IsDrity = true; }
			if (IsDrity) {
				EditorUtility.SetDirty(TargetMaterial);
				Debug.Log($"[VRSuya] {TargetMaterial.name} 머테리얼의 GPU 인스턴싱 프로퍼티가 변경되었습니다");
			}
		}

		/// <summary>해당 머테리얼에서 GI 프로퍼티 값을 일괄 변경합니다.</summary>
		void UpdateGlobalIlluminationPropertys(Material TargetMaterial) {
			bool IsDrity = false;
			MaterialGlobalIlluminationFlags GlobalIlluminationFlags = MaterialGlobalIlluminationFlags.BakedEmissive;
			bool DoubleSidedGI = true;
			if (TargetMaterial.globalIlluminationFlags != GlobalIlluminationFlags) { TargetMaterial.globalIlluminationFlags = GlobalIlluminationFlags; IsDrity = true; }
			if (TargetMaterial.doubleSidedGI != DoubleSidedGI) { TargetMaterial.doubleSidedGI = DoubleSidedGI; IsDrity = true; }
			if (IsDrity) {
				EditorUtility.SetDirty(TargetMaterial);
				Debug.Log($"[VRSuya] {TargetMaterial.name} 머테리얼의 GI 프로퍼티가 변경되었습니다");
			}
		}

		/// <summary>파일 목록에서 요청한 쉐이더 타입의 머테리얼 목록을 반환합니다.</summary>
		/// <returns>해당 쉐이더의 머테리얼 배열</returns>
		Material[] GetRequestMaterials(ShaderType TargetShader) {
			Material[] ReturnMaterials = new Material[0];
			string[] MaterialsGUID = AssetDatabase.FindAssets("t:Material", new[] { "Assets" });
			foreach (string TargetMaterialGUID in MaterialsGUID) {
				Material TargetMaterial = AssetDatabase.LoadAssetAtPath<Material>(AssetDatabase.GUIDToAssetPath(TargetMaterialGUID));
				switch (TargetShader) {
					case ShaderType.lilToon:
						if (GetShaderType(TargetMaterial) == "lilToon") ReturnMaterials = ReturnMaterials.Concat(new Material[] { TargetMaterial }).ToArray();
						break;
					case ShaderType.poiyomi:
						if (GetShaderType(TargetMaterial) == "poiyomi") ReturnMaterials = ReturnMaterials.Concat(new Material[] { TargetMaterial }).ToArray();
						break;
					case ShaderType.UnityChanToonShader:
						if (GetShaderType(TargetMaterial) == "UnityChanToonShader") ReturnMaterials = ReturnMaterials.Concat(new Material[] { TargetMaterial }).ToArray();
						break;
				}
			}
			Array.Sort(ReturnMaterials, (a, b) => string.Compare(a.name, b.name, StringComparison.Ordinal));
			return ReturnMaterials;
		}

		// 이미지 관련 메소드

		/// <summary>주어진 아바타에서 텍스쳐들을 가져옵니다.</summary>
		public void AddAvatarTextures() {
			AssetProcessor AssetProcessorInstance = new AssetProcessor();
			Texture2D[] newAvatarTexture2Ds = AssetProcessorInstance.AddAvatarTextures(TargetGameObject);
			TargetTexture2Ds = TargetTexture2Ds.Concat(newAvatarTexture2Ds).ToArray();
		}

		/// <summary>에셋에서 텍스쳐들을 가져옵니다.</summary>
		public void AddTexture2Ds() {
			List<Texture2D> ListTexture2D = new List<Texture2D>();
			string[] TextureGUIDs = AssetDatabase.FindAssets("t:Texture2D", new[] { "Assets" });
			foreach (string TargetTextureGUID in TextureGUIDs) {
				Texture2D TargetTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(AssetDatabase.GUIDToAssetPath(TargetTextureGUID));
				if (HasTextureImporter(TargetTexture)) {
					ListTexture2D.Add(TargetTexture);
				}
			}
			if (ListTexture2D.Count > 0) {
				Texture2D[] newTexture2Ds = ListTexture2D.ToArray();
				Array.Sort(newTexture2Ds, (a, b) => string.Compare(a.name, b.name, StringComparison.Ordinal));
				TargetTexture2Ds = TargetTexture2Ds.Concat(newTexture2Ds).ToArray();
			}
		}

		/// <summary>주어진 아바타에서 DXT1이 아닌 텍스쳐들을 가져옵니다.</summary>
		public void AddAvatarNotDXT1Textures() {
			AssetProcessor AssetProcessorInstance = new AssetProcessor();
			Material[] AvatarMaterials = AssetProcessorInstance.GetAvatarMaterials(TargetGameObject);
			if (AvatarMaterials.Length > 0) {
				Texture2D[] newAvatarTextures = new Texture2D[0];
				Texture2D[] AvatarNotDXT1Textures = new Texture2D[0];
				foreach (Material TargetMaterial in AvatarMaterials) {
					newAvatarTextures = newAvatarTextures.Concat(AssetProcessorInstance.GetMaterialTextures(TargetMaterial)).ToArray();
				}
				newAvatarTextures = newAvatarTextures.Distinct().ToArray();
				foreach (Texture2D TargetTexture in newAvatarTextures) {
					string AssetPath = AssetDatabase.GetAssetPath(TargetTexture);
					TextureImporter TargetTextureImporter = AssetImporter.GetAtPath(AssetPath) as TextureImporter;
					TextureImporterPlatformSettings StandaloneTextureSettings = TargetTextureImporter.GetPlatformTextureSettings("Standalone");
					if (StandaloneTextureSettings.format != TextureImporterFormat.DXT1 && TargetTextureImporter.textureType != TextureImporterType.NormalMap) {
						AvatarNotDXT1Textures = AvatarNotDXT1Textures.Concat(new Texture2D[] { TargetTexture }).Distinct().ToArray();
					}
				}
				Array.Sort(AvatarNotDXT1Textures, (a, b) => string.Compare(a.name, b.name, StringComparison.Ordinal));
				TargetTexture2Ds = TargetTexture2Ds.Concat(AvatarNotDXT1Textures).ToArray();
			}
		}

		/// <summary>에셋에서 DXT1이 아닌 텍스쳐들을 가져옵니다.</summary>
		public void AddNotDXT1Textures() {
			List<Texture2D> ListTexture2D = new List<Texture2D>();
			string[] TextureGUIDs = AssetDatabase.FindAssets("t:Texture2D", new[] { "Assets" });
			foreach (string TargetTextureGUID in TextureGUIDs) {
				Texture2D TargetTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(AssetDatabase.GUIDToAssetPath(TargetTextureGUID));
				if (HasTextureImporter(TargetTexture)) {
					ListTexture2D.Add(TargetTexture);
				}
			}
			if (ListTexture2D.Count > 0) {
				Texture2D[] AssetTexture2Ds = ListTexture2D.ToArray();
				Texture2D[] newTexture2Ds = new Texture2D[0];
				foreach (Texture2D TargetTexture in AssetTexture2Ds) {
					string AssetPath = AssetDatabase.GetAssetPath(TargetTexture);
					TextureImporter TargetTextureImporter = AssetImporter.GetAtPath(AssetPath) as TextureImporter;
					TextureImporterPlatformSettings StandaloneTextureSettings = TargetTextureImporter.GetPlatformTextureSettings("Standalone");
					if (StandaloneTextureSettings.overridden == true &&
						StandaloneTextureSettings.format != TextureImporterFormat.DXT1 &&
						TargetTextureImporter.textureType != TextureImporterType.NormalMap) {
						Debug.Log($"[VRSuya] {TargetTexture.name} : {StandaloneTextureSettings.format.ToString()}");
						newTexture2Ds = newTexture2Ds.Concat(new Texture2D[] { TargetTexture }).ToArray();
					} else if (StandaloneTextureSettings.overridden == false &&
						TargetTextureImporter.textureCompression != TextureImporterCompression.Compressed &&
						TargetTextureImporter.textureType != TextureImporterType.NormalMap) {
						Debug.Log($"[VRSuya] {TargetTexture.name} : {TargetTextureImporter.textureCompression.ToString()}");
						newTexture2Ds = newTexture2Ds.Concat(new Texture2D[] { TargetTexture }).ToArray();
					}
				}
				Array.Sort(newTexture2Ds, (a, b) => string.Compare(a.name, b.name, StringComparison.Ordinal));
				TargetTexture2Ds = TargetTexture2Ds.Concat(newTexture2Ds).ToArray();
			}
		}

		/// <summary>주어진 Texture2D가 텍스쳐인지 분석해서 반환합니다.</summary>
		/// <returns>이미지 텍스쳐 여부</returns>
		bool HasTextureImporter(Texture2D TargetTexture) {
			string AssetPath = AssetDatabase.GetAssetPath(TargetTexture);
			if (string.IsNullOrEmpty(AssetPath)) return false;
			TextureImporter TargetTextureImporter = AssetImporter.GetAtPath(AssetPath) as TextureImporter;
			if (TargetTextureImporter) {
				return true;
			} else {
				return false;
			}
		}

		/// <summary>주어진 이미지에서 알파 값이 존재하는지 여부를 분석해서 반환합니다.</summary>
		/// <returns>해당 이미지 알파 값 존재 여부</returns>
		bool HasAlphaInImage(Texture2D TargetTexture) {
			if (!TargetTexture) return false;
			string AssetPath = AssetDatabase.GetAssetPath(TargetTexture);
			TextureImporter TargetTextureImporter = AssetImporter.GetAtPath(AssetPath) as TextureImporter;
			if (!TargetTextureImporter) {
				return false;
			}
			bool ExistAlphaIsTransparency = TargetTextureImporter.alphaIsTransparency;
			TextureImporterCompression ExistCompression = TargetTextureImporter.textureCompression;
			bool ExsitStandaloneTextureSettingOverridden = TargetTextureImporter.GetPlatformTextureSettings("Standalone").overridden;
			TargetTextureImporter.alphaIsTransparency = true;
			TargetTextureImporter.textureCompression = TextureImporterCompression.Uncompressed;
			TargetTextureImporter.GetPlatformTextureSettings("Standalone").overridden = false;
			TargetTextureImporter.SaveAndReimport();
			Color[] Pixels = TargetTexture.GetPixels();
			bool hasAlpha = false;
			foreach (Color pixel in Pixels) {
				if (pixel.a < 1.0f) {
					hasAlpha = true;
					break;
				}
			}
			TargetTextureImporter.alphaIsTransparency = ExistAlphaIsTransparency;
			TargetTextureImporter.textureCompression = ExistCompression;
			TargetTextureImporter.GetPlatformTextureSettings("Standalone").overridden = ExsitStandaloneTextureSettingOverridden;
			TargetTextureImporter.SaveAndReimport();
			if (!TargetTexture.name.Contains("Transparent", StringComparison.OrdinalIgnoreCase) && hasAlpha) {
				Debug.LogWarning($"[VRSuya] {TargetTexture.name} 일반 텍스쳐가 투명 입니다");
			} else if (TargetTexture.name.Contains("Transparent", StringComparison.OrdinalIgnoreCase) && !hasAlpha) {
				Debug.LogWarning($"[VRSuya] {TargetTexture.name} 투명 텍스쳐가 불투명 입니다");
			}
			return hasAlpha;
		}

		/// <summary>주어진 이미지가 RGB 여부를 분석해서 반환합니다.</summary>
		/// <returns>해당 이미지 RGB 여부</returns>
		bool IsTextureRGB(Texture2D TargetTexture) {
			if (!TargetTexture) return false;
			Color[] Pixels = TargetTexture.GetPixels();
			bool IsRGB = false;
			foreach (Color Pixel in Pixels) {
				if (Pixel.r != Pixel.g || Pixel.g != Pixel.b) {
					IsRGB = true;
				}
			}
			if (!TargetTexture.name.Contains("Base", StringComparison.OrdinalIgnoreCase) && 
				!TargetTexture.name.Contains("Matcap", StringComparison.OrdinalIgnoreCase) && 
				!TargetTexture.name.Contains("Normal", StringComparison.OrdinalIgnoreCase) && 
				IsRGB) {
				Debug.LogWarning($"[VRSuya] {TargetTexture.name} 텍스쳐가 RGB 플래그로 설정 되었습니다!");
			}
			return IsRGB;
		}

		/// <summary>주어진 이미지가 노멀 맵인지 분석해서 반환합니다.</summary>
		/// <returns>해당 이미지 노멀 맵 여부</returns>
		bool IsTextureNormalMap(Texture2D TargetTexture) {
			if (!TargetTexture) return false;
			Color ReferenceColor = new Color(0.5f, 0.5f, 1.0f);
			float Tolerance = 0.3f;
			Color[] Pixels = TargetTexture.GetPixels();
			int ClosePixelCount = 0;
			float ToleranceSquared = Tolerance * Tolerance;
			foreach (Color Pixel in Pixels) {
				if (IsColorCloseToReference(Pixel, ReferenceColor, ToleranceSquared)) {
					ClosePixelCount++;
				}
			}
			float ClosePixelRatio = (float)ClosePixelCount / Pixels.Length;
			bool IsNormal = ClosePixelRatio > 0.5f;
			if (!TargetTexture.name.Contains("Normal", StringComparison.OrdinalIgnoreCase) && IsNormal) {
				Debug.LogWarning($"[VRSuya] {TargetTexture.name} 일반 텍스쳐의 노멀 중앙값 비율 : {Math.Truncate(ClosePixelRatio * 100)}%");
			} else if (TargetTexture.name.Contains("Normal", StringComparison.OrdinalIgnoreCase) && !IsNormal) {
				Debug.LogWarning($"[VRSuya] {TargetTexture.name} 노멀 텍스쳐의 노멀 중앙값 비율 : {Math.Truncate(ClosePixelRatio * 100)}%");
			}
			return IsNormal;
		}

		/// <summary>주어진 컬러가 노멀 맵 중앙 값에 가까운지 반환합니다.</summary>
		/// <returns>컬러가 노멀 중앙 값에 가까운지 여부</returns>
		bool IsColorCloseToReference(Color TargetColor, Color ReferenceColor, float ToleranceSquared) {
			float DistanceSquared = (TargetColor.r - ReferenceColor.r) * (TargetColor.r - ReferenceColor.r) +
									(TargetColor.g - ReferenceColor.g) * (TargetColor.g - ReferenceColor.g) +
									(TargetColor.b - ReferenceColor.b) * (TargetColor.b - ReferenceColor.b);
			return DistanceSquared < ToleranceSquared;
		}

		// 텍스쳐 프로퍼티

		/// <summary>해당 텍스쳐에서 텍스쳐 공유 프로퍼티 값을 일괄 변경합니다.</summary>
		void UpdateTexture2DSharedPropertys(Texture2D TargetTexture) {
			bool IsDrity = false;
			bool TargetStreamingMipmaps = true;
			bool TargetAlphaIsTransparency = false;
			TextureImporterMipFilter TargetMipmap = TextureImporterMipFilter.KaiserFilter;
			FilterMode TargetFilterMode = FilterMode.Trilinear;
			int TargetAnisoLevel = 16;
			int TargetMaxTextureSize = 1024;
			TextureImporterCompression TargetTextureCompression = TextureImporterCompression.CompressedLQ;
			bool TargetCrunchedCompression = false;
			int TargetCompressionQuality = 50;
			bool OverrideStandalone = true;
			string AssetPath = AssetDatabase.GetAssetPath(TargetTexture);
			TextureImporter TargetTextureImporter = AssetImporter.GetAtPath(AssetPath) as TextureImporter;
			if (TargetTextureImporter) {
                if (AnalyzeTextures) {
					bool ExistisReadable = TargetTextureImporter.isReadable;
					if (!ExistisReadable) {
						TargetTextureImporter.isReadable = true;
						TargetTextureImporter.SaveAndReimport();
					}
					bool HasRGB = (UpdatesRGB) ? IsTextureRGB(TargetTexture) : true;
					bool HasNormal = (UpdateNormal) ? IsTextureNormalMap(TargetTexture) : false;
					bool HasAlpha = (UpdateAlpha) ? HasAlphaInImage(TargetTexture) : false;
					if (UpdateNormal) {
						if (HasNormal) {
							if (TargetTextureImporter.textureType != TextureImporterType.NormalMap) { TargetTextureImporter.textureType = TextureImporterType.NormalMap; IsDrity = true; }
						} else {
							if (TargetTextureImporter.textureType != TextureImporterType.Default) { TargetTextureImporter.textureType = TextureImporterType.Default; IsDrity = true; }
						}
					}
					if (UpdatesRGB && TargetTextureImporter.sRGBTexture != HasRGB) { TargetTextureImporter.sRGBTexture = HasRGB; IsDrity = true; }
					if (UpdateAlpha && TargetTextureImporter.alphaIsTransparency != HasAlpha) {
						TargetTextureImporter.alphaIsTransparency = HasAlpha;
						IsDrity = true;
					} else if (UpdateAlpha && TargetTextureImporter.textureType == TextureImporterType.NormalMap && TargetTextureImporter.alphaIsTransparency != false) {
						TargetTextureImporter.alphaIsTransparency = false;
						IsDrity = true;
					}
					TargetTextureImporter.isReadable = ExistisReadable;
				}
				if (TargetTextureImporter.alphaIsTransparency != TargetAlphaIsTransparency) { TargetTextureImporter.alphaIsTransparency = TargetAlphaIsTransparency; IsDrity = true; }
				if (TargetTextureImporter.streamingMipmaps != TargetStreamingMipmaps) { TargetTextureImporter.streamingMipmaps = TargetStreamingMipmaps; IsDrity = true; }
				if (TargetTextureImporter.mipmapFilter != TargetMipmap) { TargetTextureImporter.mipmapFilter = TargetMipmap; IsDrity = true; }
				if (TargetTextureImporter.filterMode != TargetFilterMode) { TargetTextureImporter.filterMode = TargetFilterMode; IsDrity = true; }
				if (TargetTextureImporter.anisoLevel != TargetAnisoLevel) { TargetTextureImporter.anisoLevel = TargetAnisoLevel; IsDrity = true; }
				if (UpdateMaxTextureSize && TargetTextureImporter.maxTextureSize != TargetMaxTextureSize) { TargetTextureImporter.maxTextureSize = TargetMaxTextureSize; IsDrity = true; }
				if (TargetTextureImporter.textureCompression != TargetTextureCompression) { TargetTextureImporter.textureCompression = TargetTextureCompression; IsDrity = true; }
				if (TargetTextureImporter.crunchedCompression != TargetCrunchedCompression) { TargetTextureImporter.crunchedCompression = TargetCrunchedCompression; IsDrity = true; }
				if (TargetTextureImporter.compressionQuality != TargetCompressionQuality) { TargetTextureImporter.compressionQuality = TargetCompressionQuality; IsDrity = true; }
				if (UpdateOverrideStandalone) {
					if (TargetTextureImporter.GetPlatformTextureSettings("Standalone") != null) {
						TextureImporterPlatformSettings StandaloneTextureSettings = TargetTextureImporter.GetPlatformTextureSettings("Standalone");
						TextureImporterFormat TargetTextureImporterFormat = TextureImporterFormat.DXT1;
						if (TargetTextureImporter.textureType == TextureImporterType.Default && TargetTextureImporter.alphaIsTransparency) {
							TargetTextureImporterFormat = TextureImporterFormat.DXT5;
						} else if (TargetTextureImporter.textureType == TextureImporterType.NormalMap) {
							TargetTextureImporterFormat = TextureImporterFormat.DXT5;
						}
						if (StandaloneTextureSettings.overridden != OverrideStandalone) { StandaloneTextureSettings.overridden = OverrideStandalone; IsDrity = true; }
						if (UpdateMaxTextureSize && StandaloneTextureSettings.maxTextureSize != TargetMaxTextureSize) { StandaloneTextureSettings.maxTextureSize = TargetMaxTextureSize; IsDrity = true; }
						if (StandaloneTextureSettings.format != TargetTextureImporterFormat) { StandaloneTextureSettings.format = TargetTextureImporterFormat; IsDrity = true; }
						if (StandaloneTextureSettings.compressionQuality != TargetCompressionQuality) { StandaloneTextureSettings.compressionQuality = TargetCompressionQuality; IsDrity = true; }
						if (IsDrity) TargetTextureImporter.SetPlatformTextureSettings(StandaloneTextureSettings);
					} else {
						TextureImporterPlatformSettings StandaloneTextureSettings = new TextureImporterPlatformSettings();
						int newMaxTextureSize = TargetTextureImporter.maxTextureSize;
						if (UpdateMaxTextureSize) newMaxTextureSize = TargetMaxTextureSize;
						TextureImporterFormat newTextureImporterFormat = TextureImporterFormat.DXT1;
						if (TargetTextureImporter.textureType == TextureImporterType.Default && TargetTextureImporter.alphaIsTransparency) {
							newTextureImporterFormat = TextureImporterFormat.DXT5;
						} else if (TargetTextureImporter.textureType == TextureImporterType.NormalMap) {
							newTextureImporterFormat = TextureImporterFormat.DXT5;
						}
						StandaloneTextureSettings.name = "Standalone";
						StandaloneTextureSettings.overridden = true;
						StandaloneTextureSettings.maxTextureSize = newMaxTextureSize;
						StandaloneTextureSettings.format = newTextureImporterFormat;
						TargetTextureImporter.SetPlatformTextureSettings(StandaloneTextureSettings);
						IsDrity = true;
					}
				}
				if (IsDrity) {
					EditorUtility.SetDirty(TargetTexture);
					TargetTextureImporter.SaveAndReimport();
					Debug.Log($"[VRSuya] {TargetTexture.name} 텍스쳐 공유 프로퍼티가 변경되었습니다");
				}
			}
		}
	}
}
#endif