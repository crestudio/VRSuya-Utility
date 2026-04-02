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

namespace VRSuya.Utility {

	[InitializeOnLoad]
	[ExecuteInEditMode]
	public class AvatarScaler : MonoBehaviour {

		public enum Avatar {
			Airi, Aldina, Angura, Anon, Anri, Ash,
			Chiffon, Chise, Chocolat, Cygnet,
			Eku, Emmelie, EYO,
			Firina, Flare, Fuzzy,
			Glaze, Grus,
			Hakka,
			IMERIS,
			Karin, Kikyo, Kipfel, Kokoa, Koyuki, KUMALY, Kuronatu,
			Lapwing, Lazuli, Leefa, Leeme, Lime, LUMINA, Lunalitt,
			Mafuyu, Maki, Mamehinata, MANUKA, Mariel, Marron, Maya, MAYO, Merino, Miko, Milfy, Milk, Milltina, Minahoshi, Minase, Mint, Mir, Mishe, Moe,
			Nayu, Nehail, Nochica,
			Platinum, Plum, Pochimaru,
			Quiche,
			Rainy, Ramune, Ramune_Old, RINDO, Rokona, Rue, Rurune, Rusk,
			SELESTIA, Sephira, Shinano, Shinra, SHIRAHA, Shiratsume, Sio, Sue, Sugar, Suzuhana,
			Tien, TubeRose,
			Ukon, Usasaki, Uzuki,
			VIVH,
			Wolferia,
			Yoll, YUGI_MIYO, Yuuko
			// 검색용 신규 아바타 추가 위치
		}

		static readonly Dictionary<Avatar, float> AvatarEyeHeights = new Dictionary<Avatar, float>() {
			{ Avatar.Airi, 0.8852937f },
			{ Avatar.Aldina, 0.000000000000000000000001f },
			{ Avatar.Angura, 0.000000000000000000000001f },
			{ Avatar.Anon, 0.000000000000000000000001f },
			{ Avatar.Anri, 0.000000000000000000000001f },
			{ Avatar.Ash, 0.000000000000000000000001f },
			{ Avatar.Chiffon, 0.880152f },
			{ Avatar.Chise, 0.8845909f },
			{ Avatar.Chocolat, 0.88192f },
			{ Avatar.Cygnet, 0.000000000000000000000001f },
			{ Avatar.Eku, 0.000000000000000000000001f },
			{ Avatar.Emmelie, 0.000000000000000000000001f },
			{ Avatar.EYO, 0.000000000000000000000001f },
			{ Avatar.Firina, 0.000000000000000000000001f },
			{ Avatar.Flare, 0.000000000000000000000001f },
			{ Avatar.Fuzzy, 0.000000000000000000000001f },
			{ Avatar.Glaze, 0.000000000000000000000001f },
			{ Avatar.Grus, 0.892328f },
			{ Avatar.Hakka, 0.000000000000000000000001f },
			{ Avatar.IMERIS, 0.000000000000000000000001f },
			{ Avatar.Karin, 0.87956f },
			{ Avatar.Kikyo, 0.892182f },
			{ Avatar.Kipfel, 0.9284285f },
			{ Avatar.Kokoa, 0.8910524f },
			{ Avatar.Koyuki, 0.000000000000000000000001f },
			{ Avatar.KUMALY, 0.000000000000000000000001f },
			{ Avatar.Kuronatu, 0.000000000000000000000001f },
			{ Avatar.Lapwing, 0.000000000000000000000001f },
			{ Avatar.Lazuli, 0.000000000000000000000001f },
			{ Avatar.Leefa, 0.886995f },
			{ Avatar.Leeme, 0.000000000000000000000001f },
			{ Avatar.Lime, 0.89622f },
			{ Avatar.LUMINA, 0.000000000000000000000001f },
			{ Avatar.Lunalitt, 0.000000000000000000000001f },
			{ Avatar.Mafuyu, 0.000000000000000000000001f },
			{ Avatar.Maki, 0.000000000000000000000001f },
			{ Avatar.Mamehinata, 0.8167276f },
			{ Avatar.MANUKA, 0.8817998f },
			{ Avatar.Mariel, 0.000000000000000000000001f },
			{ Avatar.Marron, 0.000000000000000000000001f },
			{ Avatar.Maya, 0.8845845f },
			{ Avatar.MAYO, 0.000000000000000000000001f },
			{ Avatar.Merino, 0.000000000000000000000001f },
			{ Avatar.Miko, 0.8785723f },
			{ Avatar.Milfy, 0.7903f },
			{ Avatar.Milk, 0.000000000000000000000001f },
			{ Avatar.Milltina, 0.88457f },
			{ Avatar.Minahoshi, 0.000000000000000000000001f },
			{ Avatar.Minase, 0.91609f },
			{ Avatar.Mint, 0.000000000000000000000001f },
			{ Avatar.Mir, 0.000000000000000000000001f },
			{ Avatar.Mishe, 0.000000000000000000000001f },
			{ Avatar.Moe, 0.897036f },
			{ Avatar.Nayu, 0.000000000000000000000001f },
			{ Avatar.Nehail, 0.000000000000000000000001f },
			{ Avatar.Nochica, 0.000000000000000000000001f },
			{ Avatar.Platinum, 0.000000000000000000000001f },
			{ Avatar.Plum, 0.000000000000000000000001f },
			{ Avatar.Pochimaru, 0.000000000000000000000001f },
			{ Avatar.Quiche, 0.000000000000000000000001f },
			{ Avatar.Rainy, 0.000000000000000000000001f },
			{ Avatar.Ramune, 0.000000000000000000000001f },
			{ Avatar.Ramune_Old, 0.000000000000000000000001f },
			{ Avatar.RINDO, 0.000000000000000000000001f },
			{ Avatar.Rokona, 0.000000000000000000000001f },
			{ Avatar.Rue, 0.000000000000000000000001f },
			{ Avatar.Rurune, 0.000000000000000000000001f },
			{ Avatar.Rusk, 0.000000000000000000000001f },
			{ Avatar.SELESTIA, 0.8838221f },
			{ Avatar.Sephira, 0.000000000000000000000001f },
			{ Avatar.Shinano, 0.8931774f },
			{ Avatar.Shinra, 0.900882f },
			{ Avatar.SHIRAHA, 0.000000000000000000000001f },
			{ Avatar.Shiratsume, 0.000000000000000000000001f },
			{ Avatar.Sio, 0.9020135f },
			{ Avatar.Sue, 0.000000000000000000000001f },
			{ Avatar.Sugar, 0.000000000000000000000001f },
			{ Avatar.Suzuhana, 0.000000000000000000000001f },
			{ Avatar.Tien, 0.000000000000000000000001f },
			{ Avatar.TubeRose, 0.000000000000000000000001f },
			{ Avatar.Ukon, 0.889545f },
			{ Avatar.Usasaki, 0.000000000000000000000001f },
			{ Avatar.Uzuki, 0.000000000000000000000001f },
			{ Avatar.VIVH, 0.000000000000000000000001f },
			{ Avatar.Wolferia, 0.000000000000000000000001f },
			{ Avatar.Yoll, 0.000000000000000000000001f },
			{ Avatar.YUGI_MIYO, 0.000000000000000000000001f },
			{ Avatar.Yuuko, 0.000000000000000000000001f }
			// 검색용 신규 아바타 추가 위치
		};

		static readonly Dictionary<Avatar, string[]> AvatarNames = new Dictionary<Avatar, string[]>() {
			{ Avatar.Airi, new string[] { "Airi", "아이리", "愛莉" } },
			{ Avatar.Aldina, new string[] { "Aldina", "알디나", "アルディナ" } },
			{ Avatar.Angura, new string[] { "Angura", "앙그라", "アングラ" } },
			{ Avatar.Anon, new string[] { "Anon", "아논", "あのん" } },
			{ Avatar.Anri, new string[] { "Anri", "안리", "杏里" } },
			{ Avatar.Ash, new string[] { "Ash", "애쉬", "アッシュ" } },
			{ Avatar.Chiffon, new string[] { "Chiffon", "쉬폰", "シフォン" } },
			{ Avatar.Chise, new string[] { "Chise", "치세", "チセ" } },
			{ Avatar.Chocolat, new string[] { "Chocolat", "쇼콜라", "ショコラ" } },
			{ Avatar.Cygnet, new string[] { "Cygnet", "시그넷", "シグネット" } },
			{ Avatar.Eku, new string[] { "Eku", "에쿠", "エク" } },
			{ Avatar.Emmelie, new string[] { "Emmelie", "에밀리" } },
			{ Avatar.EYO, new string[] { "EYO", "이요", "イヨ" } },
			{ Avatar.Firina, new string[] { "Firina", "휘리나", "フィリナ" } },
			{ Avatar.Flare, new string[] { "Flare", "플레어", "フレア" } },
			{ Avatar.Fuzzy, new string[] { "Fuzzy", "퍼지", "ファジー" } },
			{ Avatar.Glaze, new string[] { "Glaze", "글레이즈", "ぐれーず" } },
			{ Avatar.Grus, new string[] { "Grus", "그루스" } },
			{ Avatar.Hakka, new string[] { "Hakka", "하카", "薄荷" } },
			{ Avatar.IMERIS, new string[] { "IMERIS", "이메리스", "イメリス" } },
			{ Avatar.Karin, new string[] { "Karin", "카린", "カリン" } },
			{ Avatar.Kikyo, new string[] { "Kikyo", "키쿄", "桔梗" } },
			{ Avatar.Kipfel, new string[] { "Kipfel", "키펠", "キプフェル" } },
			{ Avatar.Kokoa, new string[] { "Kokoa", "코코아", "ここあ" } },
			{ Avatar.Koyuki, new string[] { "Koyuki", "코유키", "狐雪" } },
			{ Avatar.KUMALY, new string[] { "KUMALY", "쿠마리", "クマリ" } },
			{ Avatar.Kuronatu, new string[] { "Kuronatu", "쿠로나츠", "くろなつ" } },
			{ Avatar.Lapwing, new string[] { "Lapwing", "랩윙" } },
			{ Avatar.Lazuli, new string[] { "Lazuli", "라줄리", "ラズリ" } },
			{ Avatar.Leefa, new string[] { "Leefa", "리파", "リーファ" } },
			{ Avatar.Leeme, new string[] { "Leeme", "리메", "リーメ" } },
			{ Avatar.Lime, new string[] { "Lime", "라임", "ライム" } },
			{ Avatar.LUMINA, new string[] { "LUMINA", "루미나", "ルミナ" } },
			{ Avatar.Lunalitt, new string[] { "Lunalitt", "루나릿트", "ルーナリット" } },
			{ Avatar.Mafuyu, new string[] { "Mafuyu", "마후유", "真冬" } },
			{ Avatar.Maki, new string[] { "Maki", "마키", "碼希" } },
			{ Avatar.Mamehinata, new string[] { "Mamehinata", "마메히나타", "まめひなた" } },
			{ Avatar.MANUKA, new string[] { "MANUKA", "마누카", "マヌカ" } },
			{ Avatar.Mariel, new string[] { "Mariel", "마리엘", "まりえる" } },
			{ Avatar.Marron, new string[] { "Marron", "마론", "マロン" } },
			{ Avatar.Maya, new string[] { "Maya", "마야", "舞夜" } },
			{ Avatar.MAYO, new string[] { "MAYO", "마요", "まよ" } },
			{ Avatar.Merino, new string[] { "Merino", "메리노", "メリノ" } },
			{ Avatar.Miko, new string[] { "Miko", "미코", "ミコ" } },
			{ Avatar.Milfy, new string[] { "Milfy", "미르피", "ミルフィ" } },
			{ Avatar.Milk, new string[] { "Milk", "밀크", "ミルク" } },
			{ Avatar.Milltina, new string[] { "Milltina", "밀티나", "ミルティナ" } },
			{ Avatar.Minahoshi, new string[] { "Minahoshi", "미나호시", "みなほし" } },
			{ Avatar.Minase, new string[] { "Minase", "미나세", "水瀬" } },
			{ Avatar.Mint, new string[] { "Mint", "민트", "ミント" } },
			{ Avatar.Mir, new string[] { "Mir", "미르", "ミール" } },
			{ Avatar.Mishe, new string[] { "Mishe", "미셰", "ミーシェ" } },
			{ Avatar.Moe, new string[] { "Moe", "모에", "萌" } },
			{ Avatar.Nayu, new string[] { "Nayu", "나유", "ナユ" } },
			{ Avatar.Nehail, new string[] { "Nehail", "네하일", "ネハイル" } },
			{ Avatar.Nochica, new string[] { "Nochica", "노치카", "ノーチカ" } },
			{ Avatar.Platinum, new string[] { "Platinum", "플레티늄", "プラチナ" } },
			{ Avatar.Plum, new string[] { "Plum", "플럼", "プラム" } },
			{ Avatar.Pochimaru, new string[] { "Pochimaru", "포치마루", "ぽちまる" } },
			{ Avatar.Quiche, new string[] { "Quiche", "킷슈", "キッシュ" } },
			{ Avatar.Rainy, new string[] { "Rainy", "레이니", "レイニィ" } },
			{ Avatar.Ramune, new string[] { "Ramune", "라무네", "ラムネ" } },
			{ Avatar.Ramune_Old, new string[] { "Ramune", "라무네", "ラムネ" } },
			{ Avatar.RINDO, new string[] { "RINDO", "린도", "竜胆" } },
			{ Avatar.Rokona, new string[] { "Rokona", "로코나", "ロコナ" } },
			{ Avatar.Rue, new string[] { "Rue", "루에", "ルウ" } },
			{ Avatar.Rurune, new string[] { "Rurune", "루루네", "ルルネ" } },
			{ Avatar.Rusk, new string[] { "Rusk", "러스크", "ラスク" } },
			{ Avatar.SELESTIA, new string[] { "SELESTIA", "셀레스티아", "セレスティア" } },
			{ Avatar.Sephira, new string[] { "Sephira", "세피라", "セフィラ" } },
			{ Avatar.Shinano, new string[] { "Shinano", "시나노", "しなの" } },
			{ Avatar.Shinra, new string[] { "Shinra", "신라", "森羅" } },
			{ Avatar.SHIRAHA, new string[] { "SHIRAHA", "시라하", "シラハ" } },
			{ Avatar.Shiratsume, new string[] { "Shiratsume", "시라츠메", "しらつめ" } },
			{ Avatar.Sio, new string[] { "Sio", "시오", "しお" } },
			{ Avatar.Sue, new string[] { "Sue", "스우", "透羽" } },
			{ Avatar.Sugar, new string[] { "Sugar", "슈가", "シュガ" } },
			{ Avatar.Suzuhana, new string[] { "Suzuhana", "스즈하나", "すずはな" } },
			{ Avatar.Tien, new string[] { "Tien", "티엔", "ティエン" } },
			{ Avatar.TubeRose, new string[] { "TubeRose", "튜베로즈" } },
			{ Avatar.Ukon, new string[] { "Ukon", "우콘", "右近" } },
			{ Avatar.Usasaki, new string[] { "Usasaki", "우사사키", "うささき" } },
			{ Avatar.Uzuki, new string[] { "Uzuki", "우즈키", "卯月" } },
			{ Avatar.VIVH, new string[] { "VIVH", "비브", "ビィブ" } },
			{ Avatar.Wolferia, new string[] { "Wolferia", "울페리아", "ウルフェリア" } },
			{ Avatar.Yoll, new string[] { "Yoll", "요루", "ヨル" } },
			{ Avatar.YUGI_MIYO, new string[] { "YUGI", "MIYO", "유기", "미요", "ユギ", "ミヨ" } },
			{ Avatar.Yuuko, new string[] { "Yuuko", "유우코", "幽狐" } }
			// 검색용 신규 아바타 추가 위치
		};

		public static Avatar CurrentAvatarType = Avatar.MANUKA;
		public static bool AutomaticAvatarRecognition = true;
		static int UndoGroupIndex;

		/// <summary>아바타 이름을 분석하여 자동으로 타입을 변환할지 결정합니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Automatic Avatar Recognition", priority = 1000)]
		static void SetAvatarRecognition() {
			AutomaticAvatarRecognition = !AutomaticAvatarRecognition;
			CheckAvatarMenu();
		}

		/// <summary>아바타를 지정된 타입에 맞춥니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Airi", priority = 1100)]
		static void SetAvatarTypeAiri() {
			CurrentAvatarType = Avatar.Airi;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Aldina", priority = 1100)]
		static void SetAvatarTypeAldina() {
			CurrentAvatarType = Avatar.Aldina;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Angura", priority = 1100)]
		static void SetAvatarTypeAngura() {
			CurrentAvatarType = Avatar.Angura;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Anon", priority = 1100)]
		static void SetAvatarTypeAnon() {
			CurrentAvatarType = Avatar.Anon;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Anri", priority = 1100)]
		static void SetAvatarTypeAnri() {
			CurrentAvatarType = Avatar.Anri;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Ash", priority = 1100)]
		static void SetAvatarTypeAsh() {
			CurrentAvatarType = Avatar.Ash;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Chiffon", priority = 1100)]
		static void SetAvatarTypeChiffon() {
			CurrentAvatarType = Avatar.Chiffon;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Chise", priority = 1100)]
		static void SetAvatarTypeChise() {
			CurrentAvatarType = Avatar.Chise;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Chocolat", priority = 1100)]
		static void SetAvatarTypeChocolat() {
			CurrentAvatarType = Avatar.Chocolat;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Cygnet", priority = 1100)]
		static void SetAvatarTypeCygnet() {
			CurrentAvatarType = Avatar.Cygnet;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Eku", priority = 1100)]
		static void SetAvatarTypeEku() {
			CurrentAvatarType = Avatar.Eku;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Emmelie", priority = 1100)]
		static void SetAvatarTypeEmmelie() {
			CurrentAvatarType = Avatar.Emmelie;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/EYO", priority = 1100)]
		static void SetAvatarTypeEYO() {
			CurrentAvatarType = Avatar.EYO;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Firina", priority = 1100)]
		static void SetAvatarTypeFirina() {
			CurrentAvatarType = Avatar.Firina;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Flare", priority = 1100)]
		static void SetAvatarTypeFlare() {
			CurrentAvatarType = Avatar.Flare;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Fuzzy", priority = 1100)]
		static void SetAvatarTypeFuzzy() {
			CurrentAvatarType = Avatar.Fuzzy;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Glaze", priority = 1100)]
		static void SetAvatarTypeGlaze() {
			CurrentAvatarType = Avatar.Glaze;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Grus", priority = 1100)]
		static void SetAvatarTypeGrus() {
			CurrentAvatarType = Avatar.Grus;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Hakka", priority = 1100)]
		static void SetAvatarTypeHakka() {
			CurrentAvatarType = Avatar.Hakka;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/IMERIS", priority = 1100)]
		static void SetAvatarTypeIMERIS() {
			CurrentAvatarType = Avatar.IMERIS;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Karin", priority = 1100)]
		static void SetAvatarTypeKarin() {
			CurrentAvatarType = Avatar.Karin;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Kikyo", priority = 1100)]
		static void SetAvatarTypeKikyo() {
			CurrentAvatarType = Avatar.Kikyo;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Kipfel", priority = 1100)]
		static void SetAvatarTypeKipfel() {
			CurrentAvatarType = Avatar.Kipfel;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Kokoa", priority = 1100)]
		static void SetAvatarTypeKokoa() {
			CurrentAvatarType = Avatar.Kokoa;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Koyuki", priority = 1100)]
		static void SetAvatarTypeKoyuki() {
			CurrentAvatarType = Avatar.Koyuki;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/KUMALY", priority = 1100)]
		static void SetAvatarTypeKUMALY() {
			CurrentAvatarType = Avatar.KUMALY;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Kuronatu", priority = 1100)]
		static void SetAvatarTypeKuronatu() {
			CurrentAvatarType = Avatar.Kuronatu;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Lapwing", priority = 1100)]
		static void SetAvatarTypeLapwing() {
			CurrentAvatarType = Avatar.Lapwing;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Lazuli", priority = 1100)]
		static void SetAvatarTypeLazuli() {
			CurrentAvatarType = Avatar.Lazuli;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Leefa", priority = 1100)]
		static void SetAvatarTypeLeefa() {
			CurrentAvatarType = Avatar.Leefa;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Leeme", priority = 1100)]
		static void SetAvatarTypeLeeme() {
			CurrentAvatarType = Avatar.Leeme;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Lime", priority = 1100)]
		static void SetAvatarTypeLime() {
			CurrentAvatarType = Avatar.Lime;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/LUMINA", priority = 1100)]
		static void SetAvatarTypeLUMINA() {
			CurrentAvatarType = Avatar.LUMINA;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Lunalitt", priority = 1100)]
		static void SetAvatarTypeLunalitt() {
			CurrentAvatarType = Avatar.Lunalitt;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Mafuyu", priority = 1100)]
		static void SetAvatarTypeMafuyu() {
			CurrentAvatarType = Avatar.Mafuyu;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Maki", priority = 1100)]
		static void SetAvatarTypeMaki() {
			CurrentAvatarType = Avatar.Maki;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Mamehinata", priority = 1100)]
		static void SetAvatarTypeMamehinata() {
			CurrentAvatarType = Avatar.Mamehinata;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/MANUKA", priority = 1100)]
		static void SetAvatarTypeMANUKA() {
			CurrentAvatarType = Avatar.MANUKA;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Mariel", priority = 1100)]
		static void SetAvatarTypeMariel() {
			CurrentAvatarType = Avatar.Mariel;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Marron", priority = 1100)]
		static void SetAvatarTypeMarron() {
			CurrentAvatarType = Avatar.Marron;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Maya", priority = 1100)]
		static void SetAvatarTypeMaya() {
			CurrentAvatarType = Avatar.Maya;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/MAYO", priority = 1100)]
		static void SetAvatarTypeMAYO() {
			CurrentAvatarType = Avatar.MAYO;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Merino", priority = 1100)]
		static void SetAvatarTypeMerino() {
			CurrentAvatarType = Avatar.Merino;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Miko", priority = 1100)]
		static void SetAvatarTypeMiko() {
			CurrentAvatarType = Avatar.Miko;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Milfy", priority = 1100)]
		static void SetAvatarTypeMilfy() {
			CurrentAvatarType = Avatar.Milfy;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Milk", priority = 1100)]
		static void SetAvatarTypeMilk() {
			CurrentAvatarType = Avatar.Milk;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Milltina", priority = 1100)]
		static void SetAvatarTypeMilltina() {
			CurrentAvatarType = Avatar.Milltina;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Minahoshi", priority = 1100)]
		static void SetAvatarTypeMinahoshi() {
			CurrentAvatarType = Avatar.Minahoshi;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Minase", priority = 1100)]
		static void SetAvatarTypeMinase() {
			CurrentAvatarType = Avatar.Minase;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Mint", priority = 1100)]
		static void SetAvatarTypeMint() {
			CurrentAvatarType = Avatar.Mint;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Mir", priority = 1100)]
		static void SetAvatarTypeMir() {
			CurrentAvatarType = Avatar.Mir;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Mishe", priority = 1100)]
		static void SetAvatarTypeMishe() {
			CurrentAvatarType = Avatar.Mishe;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Moe", priority = 1100)]
		static void SetAvatarTypeMoe() {
			CurrentAvatarType = Avatar.Moe;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Nayu", priority = 1100)]
		static void SetAvatarTypeNayu() {
			CurrentAvatarType = Avatar.Nayu;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Nehail", priority = 1100)]
		static void SetAvatarTypeNehail() {
			CurrentAvatarType = Avatar.Nehail;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Nochica", priority = 1100)]
		static void SetAvatarTypeNochica() {
			CurrentAvatarType = Avatar.Nochica;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Platinum", priority = 1100)]
		static void SetAvatarTypePlatinum() {
			CurrentAvatarType = Avatar.Platinum;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Plum", priority = 1100)]
		static void SetAvatarTypePlum() {
			CurrentAvatarType = Avatar.Plum;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Pochimaru", priority = 1100)]
		static void SetAvatarTypePochimaru() {
			CurrentAvatarType = Avatar.Pochimaru;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Quiche", priority = 1100)]
		static void SetAvatarTypeQuiche() {
			CurrentAvatarType = Avatar.Quiche;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Rainy", priority = 1100)]
		static void SetAvatarTypeRainy() {
			CurrentAvatarType = Avatar.Rainy;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Ramune", priority = 1100)]
		static void SetAvatarTypeRamune() {
			CurrentAvatarType = Avatar.Ramune;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Ramune(Old)", priority = 1100)]
		static void SetAvatarTypeRamune_Old() {
			CurrentAvatarType = Avatar.Ramune_Old;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/RINDO", priority = 1100)]
		static void SetAvatarTypeRINDO() {
			CurrentAvatarType = Avatar.RINDO;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Rokona", priority = 1100)]
		static void SetAvatarTypeRokona() {
			CurrentAvatarType = Avatar.Rokona;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Rue", priority = 1100)]
		static void SetAvatarTypeRue() {
			CurrentAvatarType = Avatar.Rue;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Rurune", priority = 1100)]
		static void SetAvatarTypeRurune() {
			CurrentAvatarType = Avatar.Rurune;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Rusk", priority = 1100)]
		static void SetAvatarTypeRusk() {
			CurrentAvatarType = Avatar.Rusk;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/SELESTIA", priority = 1100)]
		static void SetAvatarTypeSELESTIA() {
			CurrentAvatarType = Avatar.SELESTIA;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Sephira", priority = 1100)]
		static void SetAvatarTypeSephira() {
			CurrentAvatarType = Avatar.Sephira;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Shinano", priority = 1100)]
		static void SetAvatarTypeShinano() {
			CurrentAvatarType = Avatar.Shinano;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Shinra", priority = 1100)]
		static void SetAvatarTypeShinra() {
			CurrentAvatarType = Avatar.Shinra;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/SHIRAHA", priority = 1100)]
		static void SetAvatarTypeSHIRAHA() {
			CurrentAvatarType = Avatar.SHIRAHA;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Shiratsume", priority = 1100)]
		static void SetAvatarTypeShiratsume() {
			CurrentAvatarType = Avatar.Shiratsume;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Sio", priority = 1100)]
		static void SetAvatarTypeSio() {
			CurrentAvatarType = Avatar.Sio;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Sue", priority = 1100)]
		static void SetAvatarTypeSue() {
			CurrentAvatarType = Avatar.Sue;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Sugar", priority = 1100)]
		static void SetAvatarTypeSugar() {
			CurrentAvatarType = Avatar.Sugar;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Suzuhana", priority = 1100)]
		static void SetAvatarTypeSuzuhana() {
			CurrentAvatarType = Avatar.Suzuhana;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Tien", priority = 1100)]
		static void SetAvatarTypeTien() {
			CurrentAvatarType = Avatar.Tien;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/TubeRose", priority = 1100)]
		static void SetAvatarTypeTubeRose() {
			CurrentAvatarType = Avatar.TubeRose;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Ukon", priority = 1100)]
		static void SetAvatarTypeUkon() {
			CurrentAvatarType = Avatar.Ukon;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Usasaki", priority = 1100)]
		static void SetAvatarTypeUsasaki() {
			CurrentAvatarType = Avatar.Usasaki;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Uzuki", priority = 1100)]
		static void SetAvatarTypeUzuki() {
			CurrentAvatarType = Avatar.Uzuki;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/VIVH", priority = 1100)]
		static void SetAvatarTypeVIVH() {
			CurrentAvatarType = Avatar.VIVH;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Wolferia", priority = 1100)]
		static void SetAvatarTypeWolferia() {
			CurrentAvatarType = Avatar.Wolferia;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Yoll", priority = 1100)]
		static void SetAvatarTypeYoll() {
			CurrentAvatarType = Avatar.Yoll;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/YUGI MIYO", priority = 1100)]
		static void SetAvatarTypeYUGI_MIYO() {
			CurrentAvatarType = Avatar.YUGI_MIYO;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Yuuko", priority = 1100)]
		static void SetAvatarTypeYuuko() {
			CurrentAvatarType = Avatar.Yuuko;
			CheckAvatarMenu();
		}
		// 검색용 신규 아바타 추가 위치

		/// <summary>아바타의 키를 지정된 키에 맞춥니다.</summary>
		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/100cm", priority = 1100)]
		static void ScaleAvatar100cm() {
			ScaleAvatar(100);
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/110cm", priority = 1100)]
		static void ScaleAvatar110cm() {
			ScaleAvatar(110);
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/120cm", priority = 1100)]
		static void ScaleAvatar120cm() {
			ScaleAvatar(120);
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/130cm", priority = 1100)]
		static void ScaleAvatar130cm() {
			ScaleAvatar(130);
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/140cm", priority = 1100)]
		static void ScaleAvatar140cm() {
			ScaleAvatar(140);
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/150cm", priority = 1100)]
		static void ScaleAvatar150cm() {
			ScaleAvatar(150);
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/160cm", priority = 1100)]
		static void ScaleAvatar160cm() {
			ScaleAvatar(160);
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/170cm", priority = 1100)]
		static void ScaleAvatar170cm() {
			ScaleAvatar(170);
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/180cm", priority = 1100)]
		static void ScaleAvatar180cm() {
			ScaleAvatar(180);
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/190cm", priority = 1100)]
		static void ScaleAvatar190cm() {
			ScaleAvatar(190);
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/200cm", priority = 1100)]
		static void ScaleAvatar200cm() {
			ScaleAvatar(200);
		}

		internal static void ScaleAvatarHeight(int TargetAvatarHeight) {
			ScaleAvatar(TargetAvatarHeight);
		}

		static AvatarScaler() {
			CheckAvatarMenu();
		}

		/// <summary>아바타 메뉴의 변수 상태를 체크합니다.</summary>
		static void CheckAvatarMenu() {
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Automatic Avatar Recognition", AutomaticAvatarRecognition);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Airi", CurrentAvatarType == Avatar.Airi);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Aldina", CurrentAvatarType == Avatar.Aldina);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Angura", CurrentAvatarType == Avatar.Angura);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Anon", CurrentAvatarType == Avatar.Anon);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Anri", CurrentAvatarType == Avatar.Anri);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Ash", CurrentAvatarType == Avatar.Ash);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Chiffon", CurrentAvatarType == Avatar.Chiffon);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Chise", CurrentAvatarType == Avatar.Chise);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Chocolat", CurrentAvatarType == Avatar.Chocolat);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Cygnet", CurrentAvatarType == Avatar.Cygnet);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Eku", CurrentAvatarType == Avatar.Eku);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Emmelie", CurrentAvatarType == Avatar.Emmelie);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/EYO", CurrentAvatarType == Avatar.EYO);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Firina", CurrentAvatarType == Avatar.Firina);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Flare", CurrentAvatarType == Avatar.Flare);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Fuzzy", CurrentAvatarType == Avatar.Fuzzy);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Glaze", CurrentAvatarType == Avatar.Glaze);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Grus", CurrentAvatarType == Avatar.Grus);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Hakka", CurrentAvatarType == Avatar.Hakka);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/IMERIS", CurrentAvatarType == Avatar.IMERIS);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Karin", CurrentAvatarType == Avatar.Karin);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Kikyo", CurrentAvatarType == Avatar.Kikyo);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Kipfel", CurrentAvatarType == Avatar.Kipfel);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Kokoa", CurrentAvatarType == Avatar.Kokoa);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Koyuki", CurrentAvatarType == Avatar.Koyuki);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/KUMALY", CurrentAvatarType == Avatar.KUMALY);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Kuronatu", CurrentAvatarType == Avatar.Kuronatu);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Lapwing", CurrentAvatarType == Avatar.Lapwing);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Lazuli", CurrentAvatarType == Avatar.Lazuli);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Leefa", CurrentAvatarType == Avatar.Leefa);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Leeme", CurrentAvatarType == Avatar.Leeme);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Lime", CurrentAvatarType == Avatar.Lime);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/LUMINA", CurrentAvatarType == Avatar.LUMINA);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Lunalitt", CurrentAvatarType == Avatar.Lunalitt);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Mafuyu", CurrentAvatarType == Avatar.Mafuyu);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Maki", CurrentAvatarType == Avatar.Maki);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Mamehinata", CurrentAvatarType == Avatar.Mamehinata);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/MANUKA", CurrentAvatarType == Avatar.MANUKA);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Mariel", CurrentAvatarType == Avatar.Mariel);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Marron", CurrentAvatarType == Avatar.Marron);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Maya", CurrentAvatarType == Avatar.Maya);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/MAYO", CurrentAvatarType == Avatar.MAYO);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Merino", CurrentAvatarType == Avatar.Merino);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Miko", CurrentAvatarType == Avatar.Miko);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Milfy", CurrentAvatarType == Avatar.Milfy);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Milk", CurrentAvatarType == Avatar.Milk);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Milltina", CurrentAvatarType == Avatar.Milltina);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Minahoshi", CurrentAvatarType == Avatar.Minahoshi);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Minase", CurrentAvatarType == Avatar.Minase);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Mint", CurrentAvatarType == Avatar.Mint);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Mir", CurrentAvatarType == Avatar.Mir);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Mishe", CurrentAvatarType == Avatar.Mishe);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Moe", CurrentAvatarType == Avatar.Moe);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Nayu", CurrentAvatarType == Avatar.Nayu);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Nehail", CurrentAvatarType == Avatar.Nehail);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Nochica", CurrentAvatarType == Avatar.Nochica);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Platinum", CurrentAvatarType == Avatar.Platinum);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Plum", CurrentAvatarType == Avatar.Plum);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Pochimaru", CurrentAvatarType == Avatar.Pochimaru);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Quiche", CurrentAvatarType == Avatar.Quiche);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Rainy", CurrentAvatarType == Avatar.Rainy);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Ramune", CurrentAvatarType == Avatar.Ramune);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Ramune(Old)", CurrentAvatarType == Avatar.Ramune_Old);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/RINDO", CurrentAvatarType == Avatar.RINDO);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Rokona", CurrentAvatarType == Avatar.Rokona);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Rue", CurrentAvatarType == Avatar.Rue);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Rurune", CurrentAvatarType == Avatar.Rurune);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Rusk", CurrentAvatarType == Avatar.Rusk);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/SELESTIA", CurrentAvatarType == Avatar.SELESTIA);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Sephira", CurrentAvatarType == Avatar.Sephira);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Shinano", CurrentAvatarType == Avatar.Shinano);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Shinra", CurrentAvatarType == Avatar.Shinra);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/SHIRAHA", CurrentAvatarType == Avatar.SHIRAHA);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Shiratsume", CurrentAvatarType == Avatar.Shiratsume);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Sio", CurrentAvatarType == Avatar.Sio);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Sue", CurrentAvatarType == Avatar.Sue);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Sugar", CurrentAvatarType == Avatar.Sugar);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Suzuhana", CurrentAvatarType == Avatar.Suzuhana);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Tien", CurrentAvatarType == Avatar.Tien);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/TubeRose", CurrentAvatarType == Avatar.TubeRose);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Ukon", CurrentAvatarType == Avatar.Ukon);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Usasaki", CurrentAvatarType == Avatar.Usasaki);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Uzuki", CurrentAvatarType == Avatar.Uzuki);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/VIVH", CurrentAvatarType == Avatar.VIVH);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Wolferia", CurrentAvatarType == Avatar.Wolferia);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Yoll", CurrentAvatarType == Avatar.Yoll);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/YUGI MIYO", CurrentAvatarType == Avatar.YUGI_MIYO);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Yuuko", CurrentAvatarType == Avatar.Yuuko);
			// 검색용 신규 아바타 추가 위치
		}

		/// <summary>지정된 키를 목표로 아바타 스케일을 변경합니다.</summary>
		static void ScaleAvatar(int TargetHeight) {
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
					Debug.Log($"[VRSuya] Set the height of {AvatarObject.name} avatar to {TargetHeight}cm");
				}
				CheckAvatarMenu();
				SceneView.RepaintAll();
			}
		}

		/// <summary>아바타 이름을 분석하여 어떤 아바타인지 반환합니다.</summary>
		/// <returns>아바타 타입</returns>
		static Avatar GetCurrentAvatarType(VRC_AvatarDescriptor TargetAvatarDescriptor) {
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
		static void ScaleAvatarTransform(GameObject TargetAvatar, float TargetScale) {
			Transform TargetAvatarTransform = TargetAvatar.transform;
			Undo.RecordObject(TargetAvatarTransform, "Changed Avatar Transform");
			TargetAvatarTransform.localScale = TargetAvatarTransform.localScale * TargetScale;
			EditorUtility.SetDirty(TargetAvatarTransform);
			Undo.CollapseUndoOperations(UndoGroupIndex);
		}

		/// <summary>아바타의 뷰 포지션을 변경합니다.</summary>
		static void ScaleAvatarViewPosition(VRC_AvatarDescriptor TargetAvatarDescriptor, float TargetScale) {
			Undo.RecordObject(TargetAvatarDescriptor, "Changed Avatar View Position");
			TargetAvatarDescriptor.ViewPosition = TargetAvatarDescriptor.ViewPosition * TargetScale;
			EditorUtility.SetDirty(TargetAvatarDescriptor);
			Undo.CollapseUndoOperations(UndoGroupIndex);
		}

		/// <summary>Scene에서 조건에 맞는 VRC AvatarDescriptor 컴포넌트 아바타 1개를 반환합니다.</summary>
		/// <returns>조건에 맞는 VRC 아바타</returns>
		static VRC_AvatarDescriptor[] GetVRCAvatar() {
			VRC_AvatarDescriptor[] TargetAvatarDescriptors = GetAvatarDescriptorFromVRCSDKBuilder();
			if (TargetAvatarDescriptors.Length == 0) TargetAvatarDescriptors = GetAvatarDescriptorFromSelection();
			if (TargetAvatarDescriptors.Length == 0) TargetAvatarDescriptors = GetAvatarDescriptorFromVRCTool();
			return TargetAvatarDescriptors;
		}

		/// <summary>VRCSDK Builder에서 활성화 상태인 VRC 아바타를 반환합니다.</summary>
		/// <returns>VRCSDK Builder에서 활성화 상태인 VRC 아바타</returns>
		static VRC_AvatarDescriptor[] GetAvatarDescriptorFromVRCSDKBuilder() {
			return new VRC_AvatarDescriptor[0];
		}

		/// <summary>Unity 하이어라키에서 선택한 GameObject 중에서 VRC AvatarDescriptor 컴포넌트가 존재하는 아바타를 1개를 반환합니다.</summary>
		/// <returns>선택 중인 VRC 아바타</returns>
		static VRC_AvatarDescriptor[] GetAvatarDescriptorFromSelection() {
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
		static VRC_AvatarDescriptor[] GetAvatarDescriptorFromVRCTool() {
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
			AvatarScalerEditor AppWindow = GetWindowWithRect<AvatarScalerEditor>(new Rect(0, 0, 230, 100), true, "VRSuya AvatarScaler");
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
		}
	}
}
#endif