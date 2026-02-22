#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

/*
 * VRSuya Utility
 * Contact : vrsuya@gmail.com // Twitter : https://twitter.com/VRSuya
 */

namespace com.vrsuya.utility {

	public class ColorGradientUtility : EditorWindow {

		string BaseHEXColor = "#FFFFFF";
		string Shadow3HEXColor = "#000000";

		Color BaseColor, Shadow1Color, Shadow2Color, Shadow3Color;

		[MenuItem("Tools/VRSuya/Utility/OKLCH Color Gradient", priority = 1000)]
		static void CreateWindow() {
			ColorGradientUtility AppWindow = GetWindow<ColorGradientUtility>(true, "OKLCH Gradient Picker", true);
			AppWindow.minSize = new Vector2(300, 200);
		}

		void OnGUI() {
			EditorGUILayout.LabelField("HEX 컬러 값", EditorStyles.boldLabel);
			BaseHEXColor = EditorGUILayout.TextField("Base", BaseHEXColor);
			Shadow3HEXColor = EditorGUILayout.TextField("Shadow", Shadow3HEXColor);
			if (GUILayout.Button("중간색 추출", GUILayout.Height(30))) {
				GenerateGradients();
			}
			EditorGUILayout.Space(10);
			EditorGUILayout.LabelField("추출된 컬러 결과", EditorStyles.boldLabel);
			DisplayColorResult("Base", BaseColor);
			DisplayColorResult("Shadow 1", Shadow1Color);
			DisplayColorResult("Shadow 2", Shadow2Color);
			DisplayColorResult("Shadow 3", Shadow3Color);
		}

		void DisplayColorResult(string Label, Color TargetColor) {
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel(Label);
			EditorGUILayout.ColorField(GUIContent.none, TargetColor, false, true, false);
			EditorGUILayout.TextField(ColorUtility.ToHtmlStringRGB(TargetColor));
			EditorGUILayout.EndHorizontal();
		}

		void GenerateGradients() {
			if (ColorUtility.TryParseHtmlString(BaseHEXColor, out BaseColor) &&
				ColorUtility.TryParseHtmlString(Shadow3HEXColor, out Shadow3Color)) {
				LCH LCHStart = RGBtoLCH(BaseColor);
				LCH LCHEnd = RGBtoLCH(Shadow3Color);
				Shadow1Color = LCHtoRGB(InterpolateLCH(LCHStart, LCHEnd, 0.33f));
				Shadow2Color = LCHtoRGB(InterpolateLCH(LCHStart, LCHEnd, 0.66f));
			} else {
				Debug.LogError($"[VRSuya] HEX 형식이 올바르지 않습니다! (예: #FFFFFF)");
			}
		}

		struct LCH { public float L, C, H; }

		LCH InterpolateLCH(LCH ColorA, LCH ColorB, float Position) {
			float Hue = Mathf.LerpAngle(ColorA.H, ColorB.H, Position);
			return new LCH {
				L = Mathf.Lerp(ColorA.L, ColorB.L, Position),
				C = Mathf.Lerp(ColorA.C, ColorB.C, Position),
				H = Hue
			};
		}

		LCH RGBtoLCH(Color TargetColor) {
			float L, a, b;
			RGBtoOklab(TargetColor, out L, out a, out b);
			float C = Mathf.Sqrt(a * a + b * b);
			float H = Mathf.Atan2(b, a) * Mathf.Rad2Deg;
			return new LCH { L = L, C = C, H = H };
		}

		Color LCHtoRGB(LCH TargetLCH) {
			float a = TargetLCH.C * Mathf.Cos(TargetLCH.H * Mathf.Deg2Rad);
			float b = TargetLCH.C * Mathf.Sin(TargetLCH.H * Mathf.Deg2Rad);
			return OklabtoRGB(TargetLCH.L, a, b);
		}

		void RGBtoOklab(Color TargetColor, out float L, out float a, out float b) {
			float R = TargetColor.r, G = TargetColor.g, B = TargetColor.b;
			float l_lin = 0.4122214708f * R + 0.5363325363f * G + 0.0514459929f * B;
			float m_lin = 0.2119034982f * R + 0.6806995451f * G + 0.1073969566f * B;
			float s_lin = 0.0883024619f * R + 0.2817188376f * G + 0.6299787005f * B;
			float l_ = Mathf.Pow(l_lin, 1 / 3f);
			float m_ = Mathf.Pow(m_lin, 1 / 3f);
			float s_ = Mathf.Pow(s_lin, 1 / 3f);
			L = 0.2104542553f * l_ + 0.7936177850f * m_ - 0.0040720468f * s_;
			a = 1.9779984951f * l_ - 2.4285922050f * m_ + 0.4505937099f * s_;
			b = 0.0259040371f * l_ + 0.7827717662f * m_ - 0.8086757660f * s_;
		}

		Color OklabtoRGB(float L, float a, float b) {
			float l_ = L + 0.3963377774f * a + 0.2158037573f * b;
			float m_ = L - 0.1055613458f * a - 0.0638541728f * b;
			float s_ = L - 0.0894841775f * a - 1.2914855480f * b;
			float l_lin = l_ * l_ * l_;
			float m_lin = m_ * m_ * m_;
			float s_lin = s_ * s_ * s_;
			float R = +4.0767416621f * l_lin - 3.3077115913f * m_lin + 0.2309699292f * s_lin;
			float G = -1.2684380046f * l_lin + 2.6097574011f * m_lin - 0.3413193965f * s_lin;
			float B = -0.0041960863f * l_lin - 0.7034186147f * m_lin + 1.7076147010f * s_lin;
			return new Color(Mathf.Clamp01(R), Mathf.Clamp01(G), Mathf.Clamp01(B));
		}
	}
}
#endif