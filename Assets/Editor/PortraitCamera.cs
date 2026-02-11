#if UNITY_EDITOR
using System.Collections.Generic;
using System.Reflection;

using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

using VRC.SDKBase;

using static VRSuya.Core.Unity;
using Avatar = VRSuya.Core.Avatar;

/*
 * VRSuya Utility
 * Contact : vrsuya@gmail.com // Twitter : https://twitter.com/VRSuya
 */

namespace com.vrsuya.utility {

	public class PortraitCamera : MonoBehaviour {

		private enum ColorType {
			Levin, Macchiato,
			White, Gray, Black,
			Almond, Apricot, AshGray, CherryBlossom, CottonCandy, Cream,
			LilacFizz, Linen, Mauvelous, PaleTurquoise, PastelGrey,
			SilverSand, SkyBlue, SpringBlossom, SteelBlue, Thistle,
			Toasted, Yellow,
			Red, Green, Blue
		};

		private static Dictionary<ColorType, string> ColorList = new Dictionary<ColorType, string>() {
			{ ColorType.Levin, "#26BFBB" },
			{ ColorType.Macchiato, "#BF0000" },
			{ ColorType.White, "#FFFFFF" },
			{ ColorType.Gray, "#808080" },
			{ ColorType.Black, "#000000" },
			{ ColorType.Almond, "#F1E3D3" },
			{ ColorType.Apricot, "#F2D0A9" },
			{ ColorType.AshGray, "#A6C9C2" },
			{ ColorType.CherryBlossom, "#E0A4A4" },
			{ ColorType.CottonCandy, "#FFD3DD" },
			{ ColorType.Cream, "#F5F2EE" },
			{ ColorType.LilacFizz, "#CCA1C9" },
			{ ColorType.Linen, "#F4CFDF" },
			{ ColorType.Mauvelous, "#F3A0AD" },
			{ ColorType.PaleTurquoise, "#B6D8F2" },
			{ ColorType.PastelGrey, "#CAC6BB" },
			{ ColorType.SilverSand, "#E0DFDA" },
			{ ColorType.SkyBlue, "#9AC8EB" },
			{ ColorType.SpringBlossom, "#D8D9BC" },
			{ ColorType.SteelBlue, "#5784BA" },
			{ ColorType.Thistle, "#BEB6D9" },
			{ ColorType.Toasted, "#D7AB77" },
			{ ColorType.Yellow, "#F7F6CF" },
			{ ColorType.Red, "#FF0000" },
			{ ColorType.Green, "#00FF00" },
			{ ColorType.Blue, "#0000FF" }
		};

		/// <summary>마끼아또 아바타 프로필용 카메라를 생성합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PortraitCamera/Macchiato", priority = 1000)]
		public static void AddMacchaitoCamera() {
			AddNewCamera(ColorList[ColorType.Macchiato]);
		}

		/// <summary>레빈 아바타 프로필용 카메라를 생성합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PortraitCamera/Levin", priority = 1000)]
		public static void AddLevinCamera() {
			AddNewCamera(ColorList[ColorType.Levin]);
		}

		/// <summary>흰색 프로필용 카메라를 생성합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PortraitCamera/White", priority = 1100)]
		public static void AddWhiteCamera() {
			AddNewCamera(ColorList[ColorType.White]);
		}

		/// <summary>검은색 프로필용 카메라를 생성합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PortraitCamera/Black", priority = 1100)]
		public static void AddBlackCamera() {
			AddNewCamera(ColorList[ColorType.Black]);
		}

		/// <summary>회색 프로필용 카메라를 생성합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PortraitCamera/Gray", priority = 1100)]
		public static void AddGrayCamera() {
			AddNewCamera(ColorList[ColorType.Gray]);
		}

		/// <summary>Almond 프로필용 카메라를 생성합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PortraitCamera/Almond", priority = 1200)]
		public static void AddAlmondCamera() {
			AddNewCamera(ColorList[ColorType.Almond]);
		}

		/// <summary>Apricot 프로필용 카메라를 생성합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PortraitCamera/Apricot", priority = 1200)]
		public static void AddApricotCamera() {
			AddNewCamera(ColorList[ColorType.Apricot]);
		}

		/// <summary>Ash Gray 프로필용 카메라를 생성합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PortraitCamera/Ash Gray", priority = 1200)]
		public static void AddAshGrayCamera() {
			AddNewCamera(ColorList[ColorType.AshGray]);
		}

		/// <summary>Cherry Blossom 프로필용 카메라를 생성합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PortraitCamera/Cherry Blossom", priority = 1200)]
		public static void AddACherryBlossomCamera() {
			AddNewCamera(ColorList[ColorType.CherryBlossom]);
		}

		/// <summary>Cotton Candy 프로필용 카메라를 생성합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PortraitCamera/Cotton Candy", priority = 1200)]
		public static void AddCottonCandyCamera() {
			AddNewCamera(ColorList[ColorType.CottonCandy]);
		}

		/// <summary>Cream 프로필용 카메라를 생성합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PortraitCamera/Cream", priority = 1200)]
		public static void AddCreamCamera() {
			AddNewCamera(ColorList[ColorType.Cream]);
		}

		/// <summary>Lilac Fizz 프로필용 카메라를 생성합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PortraitCamera/Lilac Fizz", priority = 1200)]
		public static void AddLilacFizzCamera() {
			AddNewCamera(ColorList[ColorType.LilacFizz]);
		}

		/// <summary>Linen 프로필용 카메라를 생성합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PortraitCamera/Linen", priority = 1200)]
		public static void AddLinenCamera() {
			AddNewCamera(ColorList[ColorType.Linen]);
		}

		/// <summary>Mauvelous 프로필용 카메라를 생성합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PortraitCamera/Mauvelous", priority = 1200)]
		public static void AddMauvelousCamera() {
			AddNewCamera(ColorList[ColorType.Mauvelous]);
		}

		/// <summary>Pale Turquoise 프로필용 카메라를 생성합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PortraitCamera/Pale Turquoise", priority = 1200)]
		public static void AddPaleTurquoiseCamera() {
			AddNewCamera(ColorList[ColorType.PaleTurquoise]);
		}

		/// <summary>Pastel Grey 프로필용 카메라를 생성합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PortraitCamera/Pastel Grey", priority = 1200)]
		public static void AddPastelGreyCamera() {
			AddNewCamera(ColorList[ColorType.PastelGrey]);
		}

		/// <summary>Silver Sand 프로필용 카메라를 생성합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PortraitCamera/Silver Sand", priority = 1200)]
		public static void AddSilverSandCamera() {
			AddNewCamera(ColorList[ColorType.SilverSand]);
		}

		/// <summary>Sky Blue 프로필용 카메라를 생성합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PortraitCamera/Sky Blue", priority = 1200)]
		public static void AddSkyBlueCamera() {
			AddNewCamera(ColorList[ColorType.SkyBlue]);
		}

		/// <summary>SpringBlossom 프로필용 카메라를 생성합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PortraitCamera/Spring Blossom", priority = 1200)]
		public static void AddSpringBlossomCamera() {
			AddNewCamera(ColorList[ColorType.SpringBlossom]);
		}

		/// <summary>Steel Blue 프로필용 카메라를 생성합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PortraitCamera/Steel Blue", priority = 1200)]
		public static void AddSteelBlueCamera() {
			AddNewCamera(ColorList[ColorType.SteelBlue]);
		}

		/// <summary>Thistle 프로필용 카메라를 생성합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PortraitCamera/Thistle", priority = 1200)]
		public static void AddThistleCamera() {
			AddNewCamera(ColorList[ColorType.Thistle]);
		}

		/// <summary>Toasted 프로필용 카메라를 생성합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PortraitCamera/Toasted", priority = 1200)]
		public static void AddToastedCamera() {
			AddNewCamera(ColorList[ColorType.Toasted]);
		}

		/// <summary>Yellow 프로필용 카메라를 생성합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PortraitCamera/Yellow", priority = 1200)]
		public static void AddYellowCamera() {
			AddNewCamera(ColorList[ColorType.Yellow]);
		}

		/// <summary>빨간색 프로필용 카메라를 생성합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PortraitCamera/Red", priority = 1300)]
		public static void AddRedCamera() {
			AddNewCamera(ColorList[ColorType.Red]);
		}

		/// <summary>초록색 프로필용 카메라를 생성합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PortraitCamera/Green", priority = 1300)]
		public static void AddGreenCamera() {
			AddNewCamera(ColorList[ColorType.Green]);
		}

		/// <summary>파란색 프로필용 카메라를 생성합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/PortraitCamera/Blue", priority = 1300)]
		public static void AddBlueCamera() {
			AddNewCamera(ColorList[ColorType.Blue]);
		}

		/// <summary>지정된 색상으로 아바타 프로필용 카메라를 생성합니다.</summary>
		private static Camera AddNewCamera(string HEXColorCode) {
			Avatar AvatarInstance = new Avatar();
			VRC_AvatarDescriptor TargetAvatarDescriptor = AvatarInstance.GetVRCAvatarDescriptor();
			if (TargetAvatarDescriptor) {
				GameObject newGameObject = new GameObject("PortraitCamera");
				Camera newCameraComponent = newGameObject.AddComponent<Camera>();
				newCameraComponent.clearFlags = CameraClearFlags.SolidColor;
				newCameraComponent.backgroundColor = HexToColor(HEXColorCode);
				newCameraComponent.fieldOfView = 1.0f;
				newCameraComponent.nearClipPlane = 0.01f;
				newCameraComponent.renderingPath = RenderingPath.Forward;
				newGameObject.transform.position = GetCameraPosition(TargetAvatarDescriptor);
				newGameObject.transform.rotation = GetCameraRotation(TargetAvatarDescriptor);
				Undo.RegisterCreatedObjectUndo(newGameObject, "Add New PortraitCamera");
				EditorUtility.SetDirty(newCameraComponent);
				SceneView.RepaintAll();
				ApplyCustomCameraSettings(newCameraComponent);
				return newCameraComponent;
			} else {
				return null;
			}
		}

		/// <summary>아바타의 뷰 포트를 기준으로 카메라의 위치를 반환합니다.</summary>
		/// <returns>최종 카메라의 벡터 좌표</returns>
		private static Vector3 GetCameraPosition(VRC_AvatarDescriptor AvatarDescriptor) {
			Vector3 newVector3 = new Vector3(0.0f, 1.2f, 13.5f);
			Vector3 Offset = new Vector3(0.0f, -0.02f, 14.0f);
			Transform AvatarTransform = AvatarDescriptor.gameObject.transform;
			Vector3 AvatarViewPosition = AvatarTransform.position + (AvatarTransform.rotation * AvatarDescriptor.ViewPosition);
			newVector3 = AvatarViewPosition + (AvatarTransform.rotation * Offset);
			return newVector3;
		}

		/// <summary>아바타를 기준으로 카메라의 회전을 반환합니다.</summary>
		/// <returns>최종 카메라의 회전계</returns>
		private static Quaternion GetCameraRotation(VRC_AvatarDescriptor AvatarDescriptor) {
			Quaternion AvatarRotation = AvatarDescriptor.gameObject.transform.rotation;
			Quaternion ReferenceRotation = Quaternion.Euler(0, 180, 0);
			Quaternion RotationDifference = AvatarRotation * Quaternion.Inverse(ReferenceRotation);
			return RotationDifference;
		}

		private static void ApplyCustomCameraSettings(Camera TargetCamera) {
			VisualElement VRCSdkControlPanel = FindVRCSdkControlPanel();
			if (VRCSdkControlPanel == null) {
				Debug.Log("No VRCSdkControlPanel");
			}
			VisualElement ThumbnailFoldout = FindThumbnailFoldout(VRCSdkControlPanel);
			if (ThumbnailFoldout == null) {
				Debug.Log("No ThumbnailFoldout");
			}
			SetPrivateField(ThumbnailFoldout, "_useCustomCamera", true);
			SetPrivateField(ThumbnailFoldout, "_customCamera", TargetCamera);
		}

		private static VisualElement FindVRCSdkControlPanel() {
			EditorWindow[] AllWindows = Resources.FindObjectsOfTypeAll<EditorWindow>();
			foreach (EditorWindow CurrentWindow in AllWindows) {
				if (CurrentWindow.GetType().Name == "VRCSdkControlPanel") {
					return CurrentWindow.rootVisualElement;
				}
			}
			return null;
		}

		private static VisualElement FindThumbnailFoldout(VisualElement TargetVisualElement) {
			return TargetVisualElement.Q<VisualElement>(null, "ThumbnailFoldout");
		}

		private static void SetPrivateField<T>(object TargetObject, string TargetFieldName, T TargetValue) {
			FieldInfo TargetFieldInfo = TargetObject.GetType().GetField(TargetFieldName, BindingFlags.NonPublic | BindingFlags.Instance);
			if (TargetFieldInfo != null) {
				TargetFieldInfo.SetValue(TargetObject, TargetValue);
			} else {
				Debug.Log("No " + TargetFieldName);
			}
		}
	}
}
#endif