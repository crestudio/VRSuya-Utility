#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;

using UnityEditor;
using UnityEngine;

using VRC.SDKBase;

/*
 * VRSuya Utility
 * Contact : vrsuya@gmail.com // Twitter : https://twitter.com/VRSuya
 */

namespace com.vrsuya.utility {

	[InitializeOnLoad]
	[ExecuteInEditMode]
	public class AvatarScaler : MonoBehaviour {

		public enum Avatar {
			Airi,
			Chiffon, Chise, Chocolat,
			Grus,
			Karin, Kikyo, Kokoa,
			Leefa, Lime,
			Mamehinata, Manuka, Maya, Miko, Milfy, Milltina, Minase, Moe,
			Selestia, Shinano, Shinra, Sio,
			Ukon
		}

		private readonly static Dictionary<Avatar, float> AvatarEyeHeights = new Dictionary<Avatar, float>() {
			{ Avatar.Airi, 0.8852937f },
			{ Avatar.Chiffon, 0.880152f },
			{ Avatar.Chise, 0.8845909f },
			{ Avatar.Chocolat, 0.88192f },
			{ Avatar.Grus, 0.892328f },
			{ Avatar.Karin, 0.87956f },
			{ Avatar.Kikyo, 0.892182f },
			{ Avatar.Kokoa, 0.8910524f },
			{ Avatar.Leefa, 0.886995f },
			{ Avatar.Lime, 0.89622f },
			{ Avatar.Mamehinata, 0.8167276f },
			{ Avatar.Manuka, 0.8817998f },
			{ Avatar.Maya, 0.8845845f },
			{ Avatar.Miko, 0.8785723f },
			{ Avatar.Milfy, 0.7903f },
			{ Avatar.Milltina, 0.88457f },
			{ Avatar.Minase, 0.91609f },
			{ Avatar.Moe, 0.897036f },
			{ Avatar.Selestia, 0.8838221f },
			{ Avatar.Shinano, 0.8931774f },
			{ Avatar.Shinra, 0.900882f },
			{ Avatar.Sio, 0.9020135f },
			{ Avatar.Ukon, 0.889545f }
		};

		private readonly static Dictionary<Avatar, string[]> AvatarNames = new Dictionary<Avatar, string[]>() {
			{ Avatar.Airi, new string[] { "Airi", "아이리", "愛莉" } },
			{ Avatar.Chiffon, new string[] { "Chiffon", "쉬폰", "シフォン" } },
			{ Avatar.Chise, new string[] { "Chise", "치세", "チセ" } },
			{ Avatar.Chocolat, new string[] { "Chocolat", "쇼콜라", "ショコラ" } },
			{ Avatar.Grus, new string[] { "Grus", "그루스" } },
			{ Avatar.Karin, new string[] { "Karin", "카린", "カリン" } },
			{ Avatar.Kikyo, new string[] { "Kikyo", "키쿄", "桔梗" } },
			{ Avatar.Kokoa, new string[] { "Kokoa", "코코아", "ここあ" } },
			{ Avatar.Leefa, new string[] { "Leefa", "리파", "リーファ" } },
			{ Avatar.Lime, new string[] { "Lime", "라임", "ライム" } },
			{ Avatar.Mamehinata, new string[] { "Mamehinata", "마메히나타", "まめひなた" } },
			{ Avatar.Manuka, new string[] { "MANUKA", "마누카", "マヌカ" } },
			{ Avatar.Maya, new string[] { "Maya", "마야", "舞夜" } },
			{ Avatar.Miko, new string[] { "Miko", "미코", "ミコ" } },
			{ Avatar.Milfy, new string[] { "Milfy", "미르피", "ミルフィ" } },
			{ Avatar.Milltina, new string[] { "Milltina", "밀티나", "ミルティナ" } },
			{ Avatar.Minase, new string[] { "Minase", "미나세", "水瀬" } },
			{ Avatar.Moe, new string[] { "Moe", "모에", "萌" } },
			{ Avatar.Selestia, new string[] { "SELESTIA", "셀레스티아", "セレスティア" } },
			{ Avatar.Shinano, new string[] { "Shinano", "시나노", "しなの" } },
			{ Avatar.Shinra, new string[] { "Shinra", "신라", "森羅" } },
			{ Avatar.Sio, new string[] { "Sio", "시오", "しお" } },
			{ Avatar.Ukon, new string[] { "Ukon", "우콘", "右近" } }
		};

		public static Avatar CurrentAvatarType = Avatar.Kikyo;
		public static bool AutomaticAvatarRecognition = true;
		private static int UndoGroupIndex;

		/// <summary>아바타 이름을 분석하여 자동으로 타입을 변환할지 결정합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Automatic Avatar Recognition", priority = 1000)]
		public static void SetAvatarRecognition() {
			AutomaticAvatarRecognition = !AutomaticAvatarRecognition;
			CheckAvatarMenu();
			return;
		}

		/// <summary>아바타를 지정된 타입에 맞춥니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Airi", priority = 1100)]
		public static void SetAvatarTypeAiri() {
			CurrentAvatarType = Avatar.Airi;
			CheckAvatarMenu();
			return;
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Chiffon", priority = 1100)]
		public static void SetAvatarTypeChiffon() {
			CurrentAvatarType = Avatar.Chiffon;
			CheckAvatarMenu();
			return;
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Chise", priority = 1100)]
		public static void SetAvatarTypeChise() {
			CurrentAvatarType = Avatar.Chise;
			CheckAvatarMenu();
			return;
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Chocolat", priority = 1100)]
		public static void SetAvatarTypeChocolat() {
			CurrentAvatarType = Avatar.Chocolat;
			CheckAvatarMenu();
			return;
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Grus", priority = 1100)]
		public static void SetAvatarTypeGrus() {
			CurrentAvatarType = Avatar.Grus;
			CheckAvatarMenu();
			return;
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Karin", priority = 1100)]
		public static void SetAvatarTypeKarin() {
			CurrentAvatarType = Avatar.Karin;
			CheckAvatarMenu();
			return;
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Kikyo", priority = 1100)]
		public static void SetAvatarTypeKikyo() {
			CurrentAvatarType = Avatar.Kikyo;
			CheckAvatarMenu();
			return;
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Kokoa", priority = 1100)]
		public static void SetAvatarTypeKokoa() {
			CurrentAvatarType = Avatar.Kokoa;
			CheckAvatarMenu();
			return;
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Leefa", priority = 1100)]
		public static void SetAvatarTypeLeefa() {
			CurrentAvatarType = Avatar.Leefa;
			CheckAvatarMenu();
			return;
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Lime", priority = 1100)]
		public static void SetAvatarTypeLime() {
			CurrentAvatarType = Avatar.Lime;
			CheckAvatarMenu();
			return;
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Mamehinata", priority = 1100)]
		public static void SetAvatarTypeMamehinata() {
			CurrentAvatarType = Avatar.Mamehinata;
			CheckAvatarMenu();
			return;
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Manuka", priority = 1100)]
		public static void SetAvatarTypeManuka() {
			CurrentAvatarType = Avatar.Manuka;
			CheckAvatarMenu();
			return;
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Maya", priority = 1100)]
		public static void SetAvatarTypeMaya() {
			CurrentAvatarType = Avatar.Maya;
			CheckAvatarMenu();
			return;
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Miko", priority = 1100)]
		public static void SetAvatarTypeMiko() {
			CurrentAvatarType = Avatar.Miko;
			CheckAvatarMenu();
			return;
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Milfy", priority = 1100)]
		public static void SetAvatarTypeMilfy() {
			CurrentAvatarType = Avatar.Milfy;
			CheckAvatarMenu();
			return;
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Milltina", priority = 1100)]
		public static void SetAvatarTypeMilltina() {
			CurrentAvatarType = Avatar.Milltina;
			CheckAvatarMenu();
			return;
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Minase", priority = 1100)]
		public static void SetAvatarTypeMinase() {
			CurrentAvatarType = Avatar.Minase;
			CheckAvatarMenu();
			return;
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Moe", priority = 1100)]
		public static void SetAvatarTypeMoe() {
			CurrentAvatarType = Avatar.Moe;
			CheckAvatarMenu();
			return;
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Selestia", priority = 1100)]
		public static void SetAvatarTypeSelestia() {
			CurrentAvatarType = Avatar.Selestia;
			CheckAvatarMenu();
			return;
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Shinano", priority = 1100)]
		public static void SetAvatarTypeShinano() {
			CurrentAvatarType = Avatar.Shinano;
			CheckAvatarMenu();
			return;
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Shinra", priority = 1100)]
		public static void SetAvatarTypeShinra() {
			CurrentAvatarType = Avatar.Shinra;
			CheckAvatarMenu();
			return;
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Sio", priority = 1100)]
		public static void SetAvatarTypeSio() {
			CurrentAvatarType = Avatar.Sio;
			CheckAvatarMenu();
			return;
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Ukon", priority = 1100)]
		public static void SetAvatarTypeUkon() {
			CurrentAvatarType = Avatar.Ukon;
			CheckAvatarMenu();
			return;
		}

		/// <summary>아바타의 키를 지정된 키에 맞춥니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/100cm", priority = 1100)]
		public static void ScaleAvatar100cm() {
			ScaleAvatar(100);
			return;
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/110cm", priority = 1100)]
		public static void ScaleAvatar110cm() {
			ScaleAvatar(110);
			return;
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/120cm", priority = 1100)]
		public static void ScaleAvatar120cm() {
			ScaleAvatar(120);
			return;
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/130cm", priority = 1100)]
		public static void ScaleAvatar130cm() {
			ScaleAvatar(130);
			return;
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/140cm", priority = 1100)]
		public static void ScaleAvatar140cm() {
			ScaleAvatar(140);
			return;
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/150cm", priority = 1100)]
		public static void ScaleAvatar150cm() {
			ScaleAvatar(150);
			return;
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/160cm", priority = 1100)]
		public static void ScaleAvatar160cm() {
			ScaleAvatar(160);
			return;
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/170cm", priority = 1100)]
		public static void ScaleAvatar170cm() {
			ScaleAvatar(170);
			return;
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/180cm", priority = 1100)]
		public static void ScaleAvatar180cm() {
			ScaleAvatar(180);
			return;
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/190cm", priority = 1100)]
		public static void ScaleAvatar190cm() {
			ScaleAvatar(190);
			return;
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/200cm", priority = 1100)]
		public static void ScaleAvatar200cm() {
			ScaleAvatar(200);
			return;
		}

		public static void ScaleAvatarHeight(int TargetAvatarHeight) {
			ScaleAvatar(TargetAvatarHeight);
			return;
		}

		static AvatarScaler() {
			CheckAvatarMenu();
		}

		/// <summary>아바타 메뉴의 변수 상태를 체크합니다.</summary>
		private static void CheckAvatarMenu() {
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Automatic Avatar Recognition", AutomaticAvatarRecognition);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Airi", CurrentAvatarType == Avatar.Airi);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Chiffon", CurrentAvatarType == Avatar.Chiffon);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Chise", CurrentAvatarType == Avatar.Chise);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Chocolat", CurrentAvatarType == Avatar.Chocolat);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Grus", CurrentAvatarType == Avatar.Grus);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Karin", CurrentAvatarType == Avatar.Karin);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Kikyo", CurrentAvatarType == Avatar.Kikyo);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Kokoa", CurrentAvatarType == Avatar.Kokoa);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Leefa", CurrentAvatarType == Avatar.Leefa);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Lime", CurrentAvatarType == Avatar.Lime);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Mamehinata", CurrentAvatarType == Avatar.Mamehinata);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Manuka", CurrentAvatarType == Avatar.Manuka);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Maya", CurrentAvatarType == Avatar.Maya);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Miko", CurrentAvatarType == Avatar.Miko);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Milfy", CurrentAvatarType == Avatar.Milfy);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Milltina", CurrentAvatarType == Avatar.Milltina);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Minase", CurrentAvatarType == Avatar.Minase);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Moe", CurrentAvatarType == Avatar.Moe);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Selestia", CurrentAvatarType == Avatar.Selestia);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Shinano", CurrentAvatarType == Avatar.Shinano);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Shinra", CurrentAvatarType == Avatar.Shinra);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Sio", CurrentAvatarType == Avatar.Sio);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Ukon", CurrentAvatarType == Avatar.Ukon);
			return;
		}

		/// <summary>지정된 키를 목표로 아바타 스케일을 변경합니다.</summary>
		private static void ScaleAvatar(int TargetHeight) {
			if (GetVRCAvatar().Length > 0) {
				Undo.IncrementCurrentGroup();
				Undo.SetCurrentGroupName("VRSuya AvatarScaler");
				UndoGroupIndex = Undo.GetCurrentGroup();
				foreach (VRC_AvatarDescriptor TargetAvatarDescriptor in GetVRCAvatar()) {
					VRC_AvatarDescriptor AvatarDescriptor = TargetAvatarDescriptor;
					GameObject AvatarObject = AvatarDescriptor.gameObject;
					Vector3 AvatarViewPosition = AvatarDescriptor.ViewPosition;
					if (AutomaticAvatarRecognition) CurrentAvatarType = GetCurrentAvatarType(TargetAvatarDescriptor);
					float TargetEyeHeight = AvatarEyeHeights[CurrentAvatarType] * TargetHeight / 100;
					float TargetAvatarScale = TargetEyeHeight / AvatarViewPosition.y;
					ScaleAvatarTransform(AvatarObject, TargetAvatarScale);
					ScaleAvatarViewPosition(AvatarDescriptor, TargetAvatarScale);
					Debug.Log("[AvatarScaler] Set the height of " + AvatarObject.name + " avatar to " + TargetHeight + "cm");
				}
				CheckAvatarMenu();
				SceneView.RepaintAll();
			}
			return;
		}

		/// <summary>아바타 이름을 분석하여 어떤 아바타인지 반환합니다.</summary>
		/// <returns>아바타 타입</returns>
		private static Avatar GetCurrentAvatarType(VRC_AvatarDescriptor TargetAvatarDescriptor) {
			string AvatarName = TargetAvatarDescriptor.gameObject.name;
			Avatar newCurrentAvatarType = CurrentAvatarType;
			foreach (var TargetAvatarNames in AvatarNames) {
				Avatar AvatarType = TargetAvatarNames.Key;
				string[] AvatarMultiName = TargetAvatarNames.Value;
				foreach (string MultiName in AvatarMultiName) {
					if (AvatarName.Contains(MultiName, StringComparison.OrdinalIgnoreCase)) {
						newCurrentAvatarType = TargetAvatarNames.Key;
						return newCurrentAvatarType;
					}
				}
			}
			return newCurrentAvatarType;
		}

		/// <summary>아바타의 스케일을 변경합니다.</summary>
		private static void ScaleAvatarTransform(GameObject TargetAvatar, float TargetScale) {
			Transform TargetAvatarTransform = TargetAvatar.transform;
			Undo.RecordObject(TargetAvatarTransform, "Changed Avatar Transform");
			TargetAvatarTransform.localScale = TargetAvatarTransform.localScale * TargetScale;
			EditorUtility.SetDirty(TargetAvatarTransform);
			Undo.CollapseUndoOperations(UndoGroupIndex);
		}

		/// <summary>아바타의 뷰 포지션을 변경합니다.</summary>
		private static void ScaleAvatarViewPosition(VRC_AvatarDescriptor TargetAvatarDescriptor, float TargetScale) {
			Undo.RecordObject(TargetAvatarDescriptor, "Changed Avatar View Position");
			TargetAvatarDescriptor.ViewPosition = TargetAvatarDescriptor.ViewPosition * TargetScale;
			EditorUtility.SetDirty(TargetAvatarDescriptor);
			Undo.CollapseUndoOperations(UndoGroupIndex);
		}

		/// <summary>Scene에서 조건에 맞는 VRC AvatarDescriptor 컴포넌트 아바타 1개를 반환합니다.</summary>
		/// <returns>조건에 맞는 VRC 아바타</returns>
		private static VRC_AvatarDescriptor[] GetVRCAvatar() {
			VRC_AvatarDescriptor[] TargetAvatarDescriptors = GetAvatarDescriptorFromVRCSDKBuilder();
			if (TargetAvatarDescriptors.Length == 0) TargetAvatarDescriptors = GetAvatarDescriptorFromSelection();
			if (TargetAvatarDescriptors.Length == 0) TargetAvatarDescriptors = GetAvatarDescriptorFromVRCTool();
			return TargetAvatarDescriptors;
		}

		/// <summary>VRCSDK Builder에서 활성화 상태인 VRC 아바타를 반환합니다.</summary>
		/// <returns>VRCSDK Builder에서 활성화 상태인 VRC 아바타</returns>
		private static VRC_AvatarDescriptor[] GetAvatarDescriptorFromVRCSDKBuilder() {
			return new VRC_AvatarDescriptor[0];
		}

		/// <summary>Unity 하이어라키에서 선택한 GameObject 중에서 VRC AvatarDescriptor 컴포넌트가 존재하는 아바타를 1개를 반환합니다.</summary>
		/// <returns>선택 중인 VRC 아바타</returns>
		private static VRC_AvatarDescriptor[] GetAvatarDescriptorFromSelection() {
			GameObject[] SelectedGameObjects = Selection.gameObjects;
			if (SelectedGameObjects.Length == 1) {
				VRC_AvatarDescriptor SelectedVRCAvatarDescriptor = SelectedGameObjects[0].GetComponent<VRC_AvatarDescriptor>();
				if (SelectedVRCAvatarDescriptor) {
					return new VRC_AvatarDescriptor[] { SelectedVRCAvatarDescriptor };
				} else {
					return new VRC_AvatarDescriptor[0];
				}
			} else if (SelectedGameObjects.Length > 1) {
				VRC_AvatarDescriptor[] SelectedVRCAvatarDescriptor = SelectedGameObjects
					.Select(SelectedGameObject => SelectedGameObject.GetComponent<VRC_AvatarDescriptor>()).ToArray();
				return SelectedVRCAvatarDescriptor;
			} else {
				return new VRC_AvatarDescriptor[0];
			}
		}

		/// <summary>Scene에서 활성화 상태인 VRC AvatarDescriptor 컴포넌트가 존재하는 아바타를 1개를 반환합니다.</summary>
		/// <returns>Scene에서 활성화 상태인 VRC 아바타</returns>
		private static VRC_AvatarDescriptor[] GetAvatarDescriptorFromVRCTool() {
			VRC_AvatarDescriptor[] AllVRCAvatarDescriptor = VRC.Tools.FindSceneObjectsOfTypeAll<VRC_AvatarDescriptor>().ToArray();
			if (AllVRCAvatarDescriptor.Length > 0) {
				return AllVRCAvatarDescriptor.Where(Avatar => Avatar.gameObject.activeInHierarchy).ToArray();
			} else {
				return new VRC_AvatarDescriptor[0];
			}
		}
	}

	[ExecuteInEditMode]
	public class AvatarScalerEditor : EditorWindow {

		public static int TargetAvatarHeight = 150;

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Custom", priority = 1200)]
		static void CreateWindow() {
			AvatarScalerEditor AppWindow = (AvatarScalerEditor)GetWindowWithRect(typeof(AvatarScalerEditor), new Rect(0, 0, 230, 100));
			AppWindow.titleContent = new GUIContent("AvatarScaler");
			return;
		}

		void OnGUI() {
			EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.Label("Avatar Height (cm)", EditorStyles.boldLabel);
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			TargetAvatarHeight = EditorGUILayout.IntSlider(GUIContent.none, TargetAvatarHeight, 50, 250, GUILayout.Width(200));
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
			if (GUILayout.Button("Apply", GUILayout.Width(100))) {
				AvatarScaler.ScaleAvatarHeight(TargetAvatarHeight);
				Close();
			}
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
			return;
		}
	}
}
#endif