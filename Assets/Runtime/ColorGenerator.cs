#if UNITY_EDITOR
using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

using static VRSuya.Core.Unity;
using Random = UnityEngine.Random;

/*
 * VRSuya Utility
 * Contact : vrsuya@gmail.com // Twitter : https://twitter.com/VRSuya
 */

namespace com.vrsuya.utility {

	public class ColorGenerator : ScriptableObject {

		[Serializable]
		public struct ColorDelta {
			public string Name_EN;
			public string Name_KO;
			public string Name_JA;
			public ShadeType TargetShade;
			public string ReferenceColor;
			public Vector3 ColorDelta1;
			public Vector3 ColorDelta2;
			public Vector3 ColorDelta3;
			public Vector3 RimLightDelta;
			public Vector3 RimShadeDelta;
		}

		public enum ShadeType {
			Body, Hair, Cloth
		}

		private static List<ColorDelta> ColorDeltaList = new List<ColorDelta>() {
			new ColorDelta {
				Name_EN = "Glossy",
				Name_KO = "윤광",
				Name_JA = "グロッシー",
				TargetShade = ShadeType.Body,
				ReferenceColor = "#FFF0EF",
				ColorDelta1 = new Vector3(349f, 4f, -9f),
				ColorDelta2 = new Vector3(-4f, 6f, 1f),
				ColorDelta3 = new Vector3(9f, 16f, -3f),
				RimLightDelta = new Vector3(-348f, 13f, 9f),
				RimShadeDelta = new Vector3(-5f, -22f, 2f)
			}
		};
		public static int ColorDeltaListIndex = 0;
		public ColorDelta TargetColorDelta = ColorDeltaList[ColorDeltaListIndex];

		private ShadeType ShadeShadeType = ShadeType.Body;
		public int ShadeTypeIndex = 0;

		public Color ShadeColor1;
		public Color ShadeColor2;
		public Color ShadeColor3;
		public Color ShadeColor4;
		public Color RimLightColor;
		public Color RimShadeColor;

		public Material TargetMaterial;
		public Material[] TargetMaterials = new Material[0];

		// Instance 변수
		private static ColorGenerator ColorGeneratorInstance;

		// Unity Undo 변수
		private readonly string UndoGroupName = "VRSuya ColorGenerator";
		private int UndoGroupIndex;

		private void OnEnable() {
			ShadeColor1 = HexToColor(TargetColorDelta.ReferenceColor);
			ShadeColor2 = GetDeltaColor(ShadeColor1, TargetColorDelta.ColorDelta1, false);
			ShadeColor3 = GetDeltaColor(ShadeColor2, TargetColorDelta.ColorDelta2, false);
			ShadeColor4 = GetDeltaColor(ShadeColor3, TargetColorDelta.ColorDelta3, false);
			RimLightColor = GetDeltaColor(ShadeColor2, TargetColorDelta.RimLightDelta, false);
			RimShadeColor = GetDeltaColor(ShadeColor4, TargetColorDelta.RimShadeDelta, false);
			return;
		}

		public static ColorGenerator Instance {
			get {
				if (ColorGeneratorInstance == null) {
					ColorGeneratorInstance = CreateInstance<ColorGenerator>();
				}
				return ColorGeneratorInstance;
			}
		}

		public void DebugColorDelta() {
			Vector3 Delta1 = GetHSVColorDelta(ShadeColor1, ShadeColor2);
			Vector3 Delta2 = GetHSVColorDelta(ShadeColor2, ShadeColor3);
			Vector3 Delta3 = GetHSVColorDelta(ShadeColor3, ShadeColor4);
			Vector3 RimLightDelta = GetHSVColorDelta(ShadeColor2, HexToColor("FFC8C3"));
			Vector3 RimShadeDelta = GetHSVColorDelta(ShadeColor4, HexToColor("E9D1D4"));
			Debug.Log(Delta1);
			Debug.Log(Delta2);
			Debug.Log(Delta3);
			Debug.Log(RimLightDelta);
			Debug.Log(RimShadeDelta);
		}

		public void RequestGetMaterialShadeColor() {
			if (TargetMaterial) {
				if (GetShaderType(TargetMaterial) == "lilToon") {
					if (TargetMaterial.GetFloat("_UseShadow") == 1f) {
						ShadeColor1 = Color.white;
						ShadeColor2 = TargetMaterial.GetColor("_ShadowColor");
						if (TargetMaterial.GetColor("_Shadow2ndColor").a != 0f) {
							ShadeColor3 = TargetMaterial.GetColor("_Shadow2ndColor");
						} else {
							ShadeColor3 = ShadeColor2;
						}
						if (TargetMaterial.GetColor("_Shadow3rdColor").a != 0f) {
							ShadeColor4 = TargetMaterial.GetColor("_Shadow3rdColor");
						} else {
							ShadeColor4 = ShadeColor3;
						}
					} else {
						ShadeColor1 = Color.white;
						ShadeColor2 = Color.black;
						ShadeColor3 = Color.black;
						ShadeColor4 = Color.black;
					}
					if (TargetMaterial.GetFloat("_UseRim") == 1f) {
						RimLightColor = TargetMaterial.GetColor("_RimColor");
					} else {
						RimLightColor = ShadeColor2;
					}
					if (TargetMaterial.GetFloat("_UseRimShade") == 1f) {
						RimShadeColor = TargetMaterial.GetColor("_RimShadeColor");
					} else {
						RimShadeColor = ShadeColor4;
					}
				}
			}
			return;
		}

		public void RequestSetMaterialShadeColor() {
			UndoGroupIndex = InitializeUndoGroup(UndoGroupName);
			foreach (Material TargetMaterial in TargetMaterials) {
				if (TargetMaterial) {
					if (GetShaderType(TargetMaterial) == "lilToon") {
						Undo.RecordObject(TargetMaterial, UndoGroupName);
						if (ShadeColor2 != Color.black) TargetMaterial.SetColor("_ShadowColor", ShadeColor2);
						if (ShadeColor3 != Color.black) TargetMaterial.SetColor("_Shadow2ndColor", ShadeColor3);
						if (ShadeColor4 != Color.black) TargetMaterial.SetColor("_Shadow3rdColor", ShadeColor4);
						if (RimLightColor != Color.black) TargetMaterial.SetColor("_RimColor", RimLightColor);
						if (RimShadeColor != Color.black) TargetMaterial.SetColor("_RimShadeColor", RimShadeColor);
						EditorUtility.SetDirty(TargetMaterial);
						Undo.CollapseUndoOperations(UndoGroupIndex);
						Debug.Log($"[ColorGenerator] {TargetMaterial.name} 머테리얼에 설정을 적용하였습니다.");
					}
				}
			}
			return;
		}

		public void RequestCreateColorDelta() {
			ColorDelta NewColorDelta = GetNewColorDelta();
			ColorDeltaList.Add(NewColorDelta);
			TargetColorDelta = NewColorDelta;
			Debug.Log($"[ColorGenerator] {NewColorDelta.Name_EN} 설정을 생성하였습니다.");
			return;
		}

		private ColorDelta GetNewColorDelta() {
			string NewColorDeltaName = (TargetMaterial) ? TargetMaterial.name : $"ColorGenerator_{Random.Range(1000, 10000)}";
			ShadeType NewShadeType = ShadeType.Body;
			string NewReferenceColor = ColorToHex(ShadeColor1);
			Vector3 NewColorDelta1 = GetHSVColorDelta(ShadeColor1, ShadeColor2);
			Vector3 NewColorDelta2 = GetHSVColorDelta(ShadeColor2, ShadeColor3);
			Vector3 NewColorDelta3 = GetHSVColorDelta(ShadeColor3, ShadeColor4);
			Vector3 NewRimLightDelta = GetHSVColorDelta(ShadeColor2, RimLightColor);
			Vector3 NewRimShadeDelta = GetHSVColorDelta(ShadeColor4, RimShadeColor);
			return new ColorDelta {
				Name_EN = NewColorDeltaName,
				Name_KO = NewColorDeltaName,
				Name_JA = NewColorDeltaName,
				TargetShade = NewShadeType,
				ReferenceColor = NewReferenceColor,
				ColorDelta1 = NewColorDelta1,
				ColorDelta2 = NewColorDelta2,
				ColorDelta3 = NewColorDelta3,
				RimLightDelta = NewRimLightDelta,
				RimShadeDelta = NewRimShadeDelta
			};
		}

		private Vector3 GetHSVColorDelta(Color OriginalColor, Color TargetColor) {
			Vector3 OriginalHSV = ConvertRGBToHSV(OriginalColor);
			Vector3 TargetHSV = ConvertRGBToHSV(TargetColor);
			float DeltaH = Mathf.Round((TargetHSV.x - OriginalHSV.x) * 360);
			float DeltaS = Mathf.Round((TargetHSV.y - OriginalHSV.y) * 100);
			float DeltaV = Mathf.Round((TargetHSV.z - OriginalHSV.z) * 100);
			return new Vector3(DeltaH, DeltaS, DeltaV);
		}

		private static Vector3 ConvertHSVtoUnityHSV(Vector3 TargetHSV) {
			return new Vector3(TargetHSV.x / 360, TargetHSV.y / 100, TargetHSV.z / 100);
		}

		private static Color GetDeltaColor(Color TargetColor, Vector3 TargetDelta, bool Backward) {
			Vector3 TargetHSV = ConvertRGBToHSV(TargetColor);
			Vector3 UnityDeltaHSV = ConvertHSVtoUnityHSV(TargetDelta);
			float NewH = (!Backward) ? TargetHSV.x + UnityDeltaHSV.x : TargetHSV.x - UnityDeltaHSV.x;
			float NewS = (!Backward) ? TargetHSV.y + UnityDeltaHSV.y : TargetHSV.y - UnityDeltaHSV.y;
			float NewV = (!Backward) ? TargetHSV.z + UnityDeltaHSV.z : TargetHSV.z - UnityDeltaHSV.z;
			if (NewH > 1f) {
				NewH = 1f;
			} else if (NewH < 0f) {
				NewH = 0f;
			}
			if (NewS > 1f) {
				NewS = 1f;
			} else if (NewS < 0f) {
				NewS = 0f;
			}
			if (NewV > 1f) {
				NewV = 1f;
			} else if (NewV < 0f) {
				NewV = 0f;
			}
			return Color.HSVToRGB(NewH, NewS, NewV);
		}

		/// <summary>해당 머테리얼이 어떠한 쉐이더를 사용하는지 String으로 반환합니다.</summary>
		/// <returns>머테리얼이 사용하고 있는 쉐이더</returns>
		private static string GetShaderType(Material TargetMaterial) {
			string ShaderType = TargetMaterial.shader.name;
			if (TargetMaterial.shader.name.Contains("lilToon")) ShaderType = "lilToon";
			if (TargetMaterial.shader.name.Contains("poiyomi")) ShaderType = "poiyomi";
			if (TargetMaterial.shader.name.Contains("UnityChanToonShader")) ShaderType = "UnityChanToonShader";
			return ShaderType;
		}

		public void ModifiedColor1() {
			ShadeColor2 = GetDeltaColor(ShadeColor1, TargetColorDelta.ColorDelta1, false);
			ShadeColor3 = GetDeltaColor(ShadeColor2, TargetColorDelta.ColorDelta2, false);
			ShadeColor4 = GetDeltaColor(ShadeColor3, TargetColorDelta.ColorDelta3, false);
			RimLightColor = GetDeltaColor(ShadeColor2, TargetColorDelta.RimLightDelta, false);
			RimShadeColor = GetDeltaColor(ShadeColor4, TargetColorDelta.RimShadeDelta, false);
			return;
		}

		public void ModifiedColor2() {
			ShadeColor3 = GetDeltaColor(ShadeColor2, TargetColorDelta.ColorDelta2, false);
			ShadeColor4 = GetDeltaColor(ShadeColor3, TargetColorDelta.ColorDelta3, false);
			ShadeColor1 = GetDeltaColor(ShadeColor2, TargetColorDelta.ColorDelta1, true);
			RimLightColor = GetDeltaColor(ShadeColor2, TargetColorDelta.RimLightDelta, false);
			RimShadeColor = GetDeltaColor(ShadeColor4, TargetColorDelta.RimShadeDelta, false);
			return;
		}

		public void ModifiedColor3() {
			ShadeColor4 = GetDeltaColor(ShadeColor3, TargetColorDelta.ColorDelta3, false);
			ShadeColor2 = GetDeltaColor(ShadeColor3, TargetColorDelta.ColorDelta2, true);
			ShadeColor1 = GetDeltaColor(ShadeColor2, TargetColorDelta.ColorDelta1, true);
			RimLightColor = GetDeltaColor(ShadeColor2, TargetColorDelta.RimLightDelta, false);
			RimShadeColor = GetDeltaColor(ShadeColor4, TargetColorDelta.RimShadeDelta, false);
			return;
		}

		public void ModifiedColor4() {
			ShadeColor3 = GetDeltaColor(ShadeColor4, TargetColorDelta.ColorDelta3, true);
			ShadeColor2 = GetDeltaColor(ShadeColor3, TargetColorDelta.ColorDelta2, true);
			ShadeColor1 = GetDeltaColor(ShadeColor2, TargetColorDelta.ColorDelta1, true);
			RimLightColor = GetDeltaColor(ShadeColor2, TargetColorDelta.RimLightDelta, false);
			RimShadeColor = GetDeltaColor(ShadeColor4, TargetColorDelta.RimShadeDelta, false);
			return;
		}
	}
}
#endif