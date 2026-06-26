#if UNITY_EDITOR
using System;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

using VRC.SDKBase;

using VRSuya.Core;
using static VRSuya.Core.Translator;

/*
 * VRSuya Utility
 * Contact : vrsuya@gmail.com // Twitter : https://twitter.com/VRSuya
 */

namespace VRSuya.Utility {

	[InitializeOnLoad]
	[ExecuteInEditMode]
	public class AvatarScaler : ScriptableObject {

		public enum AvatarType {
			Airi, Aldina, Angura, Anon, Anri, Ash,
			Chiffon, Chise, Chocolat, Cygnet,
			Eku, Emmelie, EYO,
			Firina, Flare, Fuzzy,
			Glaze, Grus,
			Hakka,
			IMERIS,
			Karin, Kikyo, Kipfel, Kokoa, Koyuki, KUMALY, Kuronatu,
			Lapwing, Lazuli, Leefa, Leeme, Lime, LUMINA, Lunalitt,
			Mafuyu, Maki, Mamehinata, MANUKA, Mariel, Marron, Maya, MAYO, Merino, Miko, Milfy, Milk, Milltina, Minahoshi, Minase, Mint, Mir, Misaki, Mishe, Moe,
			Nayu, Nehail, Nochica,
			Platinum, Plum, Pochimaru,
			Quiche,
			Rainy, Ramune, Ramune_Old, RINDO, Rokona, Rue, Rurune, Rusk,
			SELESTIA, Sephira, Shami, Shinano, Shinra, SHIRAHA, Shiratsume, Sio, Sue, Sugar, Suzuhana,
			Tien, TubeRose,
			Ukon, Usasaki, Uzuki,
			VIVH,
			Wolferia,
			Yoll, YUGI_MIYO, Yuuko
			// 검색용 신규 아바타 추가 위치
		}

		static readonly Dictionary<AvatarType, float> AvatarEyeHeights = new Dictionary<AvatarType, float>() {
			{ AvatarType.Airi, 0.88529f },
			{ AvatarType.Aldina, 0.67435f },
			{ AvatarType.Angura, 0.74367f },
			{ AvatarType.Anon, 0.74478f },
			{ AvatarType.Anri, 0.59518f },
			{ AvatarType.Ash, 0.79229f },
			{ AvatarType.Chiffon, 0.88015f },
			{ AvatarType.Chise, 0.88459f },
			{ AvatarType.Chocolat, 0.88192f },
			{ AvatarType.Cygnet, 0.64929f },
			{ AvatarType.Eku, 0.78992f },
			{ AvatarType.Emmelie, 0.73042f },
			{ AvatarType.EYO, 0.72782f },
			{ AvatarType.Firina, 0.81974f },
			{ AvatarType.Flare, 0.80431f },
			{ AvatarType.Fuzzy, 0.82579f },
			{ AvatarType.Glaze, 0.72178f },
			{ AvatarType.Grus, 0.89232f },
			{ AvatarType.Hakka, 0.75674f },
			{ AvatarType.IMERIS, 0.70637f },
			{ AvatarType.Karin, 0.87956f },
			{ AvatarType.Kikyo, 0.89218f },
			{ AvatarType.Kipfel, 0.92842f },
			{ AvatarType.Kokoa, 0.89105f },
			{ AvatarType.Koyuki, 0.77037f },
			{ AvatarType.KUMALY, 0.81845f },
			{ AvatarType.Kuronatu, 0.65784f },
			{ AvatarType.Lapwing, 0.66969f },
			{ AvatarType.Lazuli, 0.70999f },
			{ AvatarType.Leefa, 0.88699f },
			{ AvatarType.Leeme, 0.70895f },
			{ AvatarType.Lime, 0.89622f },
			{ AvatarType.LUMINA, 0.71797f },
			{ AvatarType.Lunalitt, 0.77447f },
			{ AvatarType.Mafuyu, 0.75831f },
			{ AvatarType.Maki, 0.75799f },
			{ AvatarType.Mamehinata, 0.81672f },
			{ AvatarType.MANUKA, 0.88179f },
			{ AvatarType.Mariel, 0.77157f },
			{ AvatarType.Marron, 0.81774f },
			{ AvatarType.Maya, 0.88458f },
			{ AvatarType.MAYO, 0.78089f },
			{ AvatarType.Merino, 0.73775f },
			{ AvatarType.Miko, 0.87857f },
			{ AvatarType.Milfy, 0.79031f },
			{ AvatarType.Milk, 0.93147f },
			{ AvatarType.Milltina, 0.88457f },
			{ AvatarType.Minahoshi, 0.99447f },
			{ AvatarType.Minase, 0.91609f },
			{ AvatarType.Mint, 0.86175f },
			{ AvatarType.Mir, 0.63818f },
			{ AvatarType.Misaki, 0.66556f },
			{ AvatarType.Mishe, 0.81218f },
			{ AvatarType.Moe, 0.89703f },
			{ AvatarType.Nayu, 0.72969f },
			{ AvatarType.Nehail, 0.69458f },
			{ AvatarType.Nochica, 1.15158f },
			{ AvatarType.Platinum, 0.73878f },
			{ AvatarType.Plum, 0.80421f },
			{ AvatarType.Pochimaru, 2.36568f },
			{ AvatarType.Quiche, 0.70725f },
			{ AvatarType.Rainy, 0.76184f },
			{ AvatarType.Ramune, 0.77587f },
			{ AvatarType.Ramune_Old, 0.79858f },
			{ AvatarType.RINDO, 0.76298f },
			{ AvatarType.Rokona, 0.67214f },
			{ AvatarType.Rue, 0.90979f },
			{ AvatarType.Rurune, 0.71263f },
			{ AvatarType.Rusk, 0.83499f },
			{ AvatarType.SELESTIA, 0.88382f },
			{ AvatarType.Sephira, 0.80346f },
			{ AvatarType.Shami, 1.35346f },
			{ AvatarType.Shinano, 0.89317f },
			{ AvatarType.Shinra, 0.90088f },
			{ AvatarType.SHIRAHA, 0.79584f },
			{ AvatarType.Shiratsume, 0.78347f },
			{ AvatarType.Sio, 0.90201f },
			{ AvatarType.Sue, 0.72883f },
			{ AvatarType.Sugar, 0.76792f },
			{ AvatarType.Suzuhana, 0.75278f },
			{ AvatarType.Tien, 0.83276f },
			{ AvatarType.TubeRose, 0.68368f },
			{ AvatarType.Ukon, 0.88954f },
			{ AvatarType.Usasaki, 0.80831f },
			{ AvatarType.Uzuki, 0.77369f },
			{ AvatarType.VIVH, 0.57581f },
			{ AvatarType.Wolferia, 0.71184f },
			{ AvatarType.Yoll, 0.64192f },
			{ AvatarType.YUGI_MIYO, 0.77583f },
			{ AvatarType.Yuuko, 0.71272f }
			// 검색용 신규 아바타 추가 위치
		};

		static readonly Dictionary<AvatarType, string[]> AvatarNameDictionary = new Dictionary<AvatarType, string[]>() {
			{ AvatarType.Airi, new string[] { "Airi", "아이리", "愛莉" } },
			{ AvatarType.Aldina, new string[] { "Aldina", "알디나", "アルディナ" } },
			{ AvatarType.Angura, new string[] { "Angura", "앙그라", "アングラ" } },
			{ AvatarType.Anon, new string[] { "Anon", "아논", "あのん" } },
			{ AvatarType.Anri, new string[] { "Anri", "안리", "杏里" } },
			{ AvatarType.Ash, new string[] { "Ash", "애쉬", "アッシュ" } },
			{ AvatarType.Chiffon, new string[] { "Chiffon", "쉬폰", "シフォン" } },
			{ AvatarType.Chise, new string[] { "Chise", "치세", "チセ" } },
			{ AvatarType.Chocolat, new string[] { "Chocolat", "쇼콜라", "ショコラ" } },
			{ AvatarType.Cygnet, new string[] { "Cygnet", "시그넷", "シグネット" } },
			{ AvatarType.Eku, new string[] { "Eku", "에쿠", "エク" } },
			{ AvatarType.Emmelie, new string[] { "Emmelie", "에밀리" } },
			{ AvatarType.EYO, new string[] { "EYO", "이요", "イヨ" } },
			{ AvatarType.Firina, new string[] { "Firina", "휘리나", "フィリナ" } },
			{ AvatarType.Flare, new string[] { "Flare", "플레어", "フレア" } },
			{ AvatarType.Fuzzy, new string[] { "Fuzzy", "퍼지", "ファジー" } },
			{ AvatarType.Glaze, new string[] { "Glaze", "글레이즈", "ぐれーず" } },
			{ AvatarType.Grus, new string[] { "Grus", "그루스" } },
			{ AvatarType.Hakka, new string[] { "Hakka", "하카", "薄荷" } },
			{ AvatarType.IMERIS, new string[] { "IMERIS", "이메리스", "イメリス" } },
			{ AvatarType.Karin, new string[] { "Karin", "카린", "カリン" } },
			{ AvatarType.Kikyo, new string[] { "Kikyo", "키쿄", "桔梗" } },
			{ AvatarType.Kipfel, new string[] { "Kipfel", "키펠", "キプフェル" } },
			{ AvatarType.Kokoa, new string[] { "Kokoa", "코코아", "ここあ" } },
			{ AvatarType.Koyuki, new string[] { "Koyuki", "코유키", "狐雪" } },
			{ AvatarType.KUMALY, new string[] { "KUMALY", "쿠마리", "クマリ" } },
			{ AvatarType.Kuronatu, new string[] { "Kuronatu", "쿠로나츠", "くろなつ" } },
			{ AvatarType.Lapwing, new string[] { "Lapwing", "랩윙" } },
			{ AvatarType.Lazuli, new string[] { "Lazuli", "라줄리", "ラズリ" } },
			{ AvatarType.Leefa, new string[] { "Leefa", "리파", "リーファ" } },
			{ AvatarType.Leeme, new string[] { "Leeme", "리메", "リーメ" } },
			{ AvatarType.Lime, new string[] { "Lime", "라임", "ライム" } },
			{ AvatarType.LUMINA, new string[] { "LUMINA", "루미나", "ルミナ" } },
			{ AvatarType.Lunalitt, new string[] { "Lunalitt", "루나릿트", "ルーナリット" } },
			{ AvatarType.Mafuyu, new string[] { "Mafuyu", "마후유", "真冬" } },
			{ AvatarType.Maki, new string[] { "Maki", "마키", "碼希" } },
			{ AvatarType.Mamehinata, new string[] { "Mamehinata", "마메히나타", "まめひなた" } },
			{ AvatarType.MANUKA, new string[] { "MANUKA", "마누카", "マヌカ" } },
			{ AvatarType.Mariel, new string[] { "Mariel", "마리엘", "まりえる" } },
			{ AvatarType.Marron, new string[] { "Marron", "마론", "マロン" } },
			{ AvatarType.Maya, new string[] { "Maya", "마야", "舞夜" } },
			{ AvatarType.MAYO, new string[] { "MAYO", "마요", "まよ" } },
			{ AvatarType.Merino, new string[] { "Merino", "메리노", "メリノ" } },
			{ AvatarType.Miko, new string[] { "Miko", "미코", "ミコ" } },
			{ AvatarType.Milfy, new string[] { "Milfy", "미르피", "ミルフィ" } },
			{ AvatarType.Milk, new string[] { "Milk", "밀크", "ミルク" } },
			{ AvatarType.Milltina, new string[] { "Milltina", "밀티나", "ミルティナ" } },
			{ AvatarType.Minahoshi, new string[] { "Minahoshi", "미나호시", "みなほし" } },
			{ AvatarType.Minase, new string[] { "Minase", "미나세", "水瀬" } },
			{ AvatarType.Mint, new string[] { "Mint", "민트", "ミント" } },
			{ AvatarType.Mir, new string[] { "Mir", "미르", "ミール" } },
			{ AvatarType.Misaki, new string[] { "Misaki", "미사키", "海咲" } },
			{ AvatarType.Mishe, new string[] { "Mishe", "미셰", "ミーシェ" } },
			{ AvatarType.Moe, new string[] { "Moe", "모에", "萌" } },
			{ AvatarType.Nayu, new string[] { "Nayu", "나유", "ナユ" } },
			{ AvatarType.Nehail, new string[] { "Nehail", "네하일", "ネハイル" } },
			{ AvatarType.Nochica, new string[] { "Nochica", "노치카", "ノーチカ" } },
			{ AvatarType.Platinum, new string[] { "Platinum", "플레티늄", "プラチナ" } },
			{ AvatarType.Plum, new string[] { "Plum", "플럼", "プラム" } },
			{ AvatarType.Pochimaru, new string[] { "Pochimaru", "포치마루", "ぽちまる" } },
			{ AvatarType.Quiche, new string[] { "Quiche", "킷슈", "キッシュ" } },
			{ AvatarType.Rainy, new string[] { "Rainy", "레이니", "レイニィ" } },
			{ AvatarType.Ramune, new string[] { "Ramune", "라무네", "ラムネ" } },
			{ AvatarType.Ramune_Old, new string[] { "Ramune", "라무네", "ラムネ" } },
			{ AvatarType.RINDO, new string[] { "RINDO", "린도", "竜胆" } },
			{ AvatarType.Rokona, new string[] { "Rokona", "로코나", "ロコナ" } },
			{ AvatarType.Rue, new string[] { "Rue", "루에", "ルウ" } },
			{ AvatarType.Rurune, new string[] { "Rurune", "루루네", "ルルネ" } },
			{ AvatarType.Rusk, new string[] { "Rusk", "러스크", "ラスク" } },
			{ AvatarType.SELESTIA, new string[] { "SELESTIA", "셀레스티아", "セレスティア" } },
			{ AvatarType.Sephira, new string[] { "Sephira", "세피라", "セフィラ" } },
			{ AvatarType.Shami, new string[] { "Shami", "샤미", "シャミ" } },
			{ AvatarType.Shinano, new string[] { "Shinano", "시나노", "しなの" } },
			{ AvatarType.Shinra, new string[] { "Shinra", "신라", "森羅" } },
			{ AvatarType.SHIRAHA, new string[] { "SHIRAHA", "시라하", "シラハ" } },
			{ AvatarType.Shiratsume, new string[] { "Shiratsume", "시라츠메", "しらつめ" } },
			{ AvatarType.Sio, new string[] { "Sio", "시오", "しお" } },
			{ AvatarType.Sue, new string[] { "Sue", "스우", "透羽" } },
			{ AvatarType.Sugar, new string[] { "Sugar", "슈가", "シュガ" } },
			{ AvatarType.Suzuhana, new string[] { "Suzuhana", "스즈하나", "すずはな" } },
			{ AvatarType.Tien, new string[] { "Tien", "티엔", "ティエン" } },
			{ AvatarType.TubeRose, new string[] { "TubeRose", "튜베로즈" } },
			{ AvatarType.Ukon, new string[] { "Ukon", "우콘", "右近" } },
			{ AvatarType.Usasaki, new string[] { "Usasaki", "우사사키", "うささき" } },
			{ AvatarType.Uzuki, new string[] { "Uzuki", "우즈키", "卯月" } },
			{ AvatarType.VIVH, new string[] { "VIVH", "비브", "ビィブ" } },
			{ AvatarType.Wolferia, new string[] { "Wolferia", "울페리아", "ウルフェリア" } },
			{ AvatarType.Yoll, new string[] { "Yoll", "요루", "ヨル" } },
			{ AvatarType.YUGI_MIYO, new string[] { "YUGI", "MIYO", "유기", "미요", "ユギ", "ミヨ" } },
			{ AvatarType.Yuuko, new string[] { "Yuuko", "유우코", "幽狐" } }
			// 검색용 신규 아바타 추가 위치
		};

		public static AvatarType CurrentAvatarType = AvatarType.Shinano;
		public static bool AutomaticAvatarRecognition = true;

		const string UndoGroupName = "VRSuya AvatarScaler";
		static int UndoGroupIndex;

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Automatic Avatar Recognition", priority = 1000)]
		static void SetAvatarRecognition() {
			AutomaticAvatarRecognition = !AutomaticAvatarRecognition;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Airi", priority = 1100)]
		static void SetAvatarTypeAiri() {
			CurrentAvatarType = AvatarType.Airi;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Aldina", priority = 1100)]
		static void SetAvatarTypeAldina() {
			CurrentAvatarType = AvatarType.Aldina;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Angura", priority = 1100)]
		static void SetAvatarTypeAngura() {
			CurrentAvatarType = AvatarType.Angura;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Anon", priority = 1100)]
		static void SetAvatarTypeAnon() {
			CurrentAvatarType = AvatarType.Anon;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Anri", priority = 1100)]
		static void SetAvatarTypeAnri() {
			CurrentAvatarType = AvatarType.Anri;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Ash", priority = 1100)]
		static void SetAvatarTypeAsh() {
			CurrentAvatarType = AvatarType.Ash;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Chiffon", priority = 1100)]
		static void SetAvatarTypeChiffon() {
			CurrentAvatarType = AvatarType.Chiffon;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Chise", priority = 1100)]
		static void SetAvatarTypeChise() {
			CurrentAvatarType = AvatarType.Chise;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Chocolat", priority = 1100)]
		static void SetAvatarTypeChocolat() {
			CurrentAvatarType = AvatarType.Chocolat;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Cygnet", priority = 1100)]
		static void SetAvatarTypeCygnet() {
			CurrentAvatarType = AvatarType.Cygnet;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Eku", priority = 1100)]
		static void SetAvatarTypeEku() {
			CurrentAvatarType = AvatarType.Eku;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Emmelie", priority = 1100)]
		static void SetAvatarTypeEmmelie() {
			CurrentAvatarType = AvatarType.Emmelie;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/EYO", priority = 1100)]
		static void SetAvatarTypeEYO() {
			CurrentAvatarType = AvatarType.EYO;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Firina", priority = 1100)]
		static void SetAvatarTypeFirina() {
			CurrentAvatarType = AvatarType.Firina;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Flare", priority = 1100)]
		static void SetAvatarTypeFlare() {
			CurrentAvatarType = AvatarType.Flare;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Fuzzy", priority = 1100)]
		static void SetAvatarTypeFuzzy() {
			CurrentAvatarType = AvatarType.Fuzzy;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Glaze", priority = 1100)]
		static void SetAvatarTypeGlaze() {
			CurrentAvatarType = AvatarType.Glaze;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Grus", priority = 1100)]
		static void SetAvatarTypeGrus() {
			CurrentAvatarType = AvatarType.Grus;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Hakka", priority = 1100)]
		static void SetAvatarTypeHakka() {
			CurrentAvatarType = AvatarType.Hakka;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/IMERIS", priority = 1100)]
		static void SetAvatarTypeIMERIS() {
			CurrentAvatarType = AvatarType.IMERIS;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Karin", priority = 1100)]
		static void SetAvatarTypeKarin() {
			CurrentAvatarType = AvatarType.Karin;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Kikyo", priority = 1100)]
		static void SetAvatarTypeKikyo() {
			CurrentAvatarType = AvatarType.Kikyo;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Kipfel", priority = 1100)]
		static void SetAvatarTypeKipfel() {
			CurrentAvatarType = AvatarType.Kipfel;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Kokoa", priority = 1100)]
		static void SetAvatarTypeKokoa() {
			CurrentAvatarType = AvatarType.Kokoa;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Koyuki", priority = 1100)]
		static void SetAvatarTypeKoyuki() {
			CurrentAvatarType = AvatarType.Koyuki;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/KUMALY", priority = 1100)]
		static void SetAvatarTypeKUMALY() {
			CurrentAvatarType = AvatarType.KUMALY;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Kuronatu", priority = 1100)]
		static void SetAvatarTypeKuronatu() {
			CurrentAvatarType = AvatarType.Kuronatu;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Lapwing", priority = 1100)]
		static void SetAvatarTypeLapwing() {
			CurrentAvatarType = AvatarType.Lapwing;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Lazuli", priority = 1100)]
		static void SetAvatarTypeLazuli() {
			CurrentAvatarType = AvatarType.Lazuli;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Leefa", priority = 1100)]
		static void SetAvatarTypeLeefa() {
			CurrentAvatarType = AvatarType.Leefa;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Leeme", priority = 1100)]
		static void SetAvatarTypeLeeme() {
			CurrentAvatarType = AvatarType.Leeme;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Lime", priority = 1100)]
		static void SetAvatarTypeLime() {
			CurrentAvatarType = AvatarType.Lime;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/LUMINA", priority = 1100)]
		static void SetAvatarTypeLUMINA() {
			CurrentAvatarType = AvatarType.LUMINA;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Lunalitt", priority = 1100)]
		static void SetAvatarTypeLunalitt() {
			CurrentAvatarType = AvatarType.Lunalitt;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Mafuyu", priority = 1100)]
		static void SetAvatarTypeMafuyu() {
			CurrentAvatarType = AvatarType.Mafuyu;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Maki", priority = 1100)]
		static void SetAvatarTypeMaki() {
			CurrentAvatarType = AvatarType.Maki;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Mamehinata", priority = 1100)]
		static void SetAvatarTypeMamehinata() {
			CurrentAvatarType = AvatarType.Mamehinata;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/MANUKA", priority = 1100)]
		static void SetAvatarTypeMANUKA() {
			CurrentAvatarType = AvatarType.MANUKA;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Mariel", priority = 1100)]
		static void SetAvatarTypeMariel() {
			CurrentAvatarType = AvatarType.Mariel;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Marron", priority = 1100)]
		static void SetAvatarTypeMarron() {
			CurrentAvatarType = AvatarType.Marron;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Maya", priority = 1100)]
		static void SetAvatarTypeMaya() {
			CurrentAvatarType = AvatarType.Maya;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/MAYO", priority = 1100)]
		static void SetAvatarTypeMAYO() {
			CurrentAvatarType = AvatarType.MAYO;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Merino", priority = 1100)]
		static void SetAvatarTypeMerino() {
			CurrentAvatarType = AvatarType.Merino;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Miko", priority = 1100)]
		static void SetAvatarTypeMiko() {
			CurrentAvatarType = AvatarType.Miko;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Milfy", priority = 1100)]
		static void SetAvatarTypeMilfy() {
			CurrentAvatarType = AvatarType.Milfy;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Milk", priority = 1100)]
		static void SetAvatarTypeMilk() {
			CurrentAvatarType = AvatarType.Milk;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Milltina", priority = 1100)]
		static void SetAvatarTypeMilltina() {
			CurrentAvatarType = AvatarType.Milltina;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Minahoshi", priority = 1100)]
		static void SetAvatarTypeMinahoshi() {
			CurrentAvatarType = AvatarType.Minahoshi;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Minase", priority = 1100)]
		static void SetAvatarTypeMinase() {
			CurrentAvatarType = AvatarType.Minase;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Mint", priority = 1100)]
		static void SetAvatarTypeMint() {
			CurrentAvatarType = AvatarType.Mint;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Mir", priority = 1100)]
		static void SetAvatarTypeMir() {
			CurrentAvatarType = AvatarType.Mir;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Misaki", priority = 1100)]
		static void SetAvatarTypeMisaki() {
			CurrentAvatarType = AvatarType.Misaki;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Mishe", priority = 1100)]
		static void SetAvatarTypeMishe() {
			CurrentAvatarType = AvatarType.Mishe;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Moe", priority = 1100)]
		static void SetAvatarTypeMoe() {
			CurrentAvatarType = AvatarType.Moe;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Nayu", priority = 1100)]
		static void SetAvatarTypeNayu() {
			CurrentAvatarType = AvatarType.Nayu;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Nehail", priority = 1100)]
		static void SetAvatarTypeNehail() {
			CurrentAvatarType = AvatarType.Nehail;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Nochica", priority = 1100)]
		static void SetAvatarTypeNochica() {
			CurrentAvatarType = AvatarType.Nochica;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Platinum", priority = 1100)]
		static void SetAvatarTypePlatinum() {
			CurrentAvatarType = AvatarType.Platinum;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Plum", priority = 1100)]
		static void SetAvatarTypePlum() {
			CurrentAvatarType = AvatarType.Plum;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Pochimaru", priority = 1100)]
		static void SetAvatarTypePochimaru() {
			CurrentAvatarType = AvatarType.Pochimaru;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Quiche", priority = 1100)]
		static void SetAvatarTypeQuiche() {
			CurrentAvatarType = AvatarType.Quiche;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Rainy", priority = 1100)]
		static void SetAvatarTypeRainy() {
			CurrentAvatarType = AvatarType.Rainy;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Ramune", priority = 1100)]
		static void SetAvatarTypeRamune() {
			CurrentAvatarType = AvatarType.Ramune;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Ramune(Old)", priority = 1100)]
		static void SetAvatarTypeRamune_Old() {
			CurrentAvatarType = AvatarType.Ramune_Old;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/RINDO", priority = 1100)]
		static void SetAvatarTypeRINDO() {
			CurrentAvatarType = AvatarType.RINDO;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Rokona", priority = 1100)]
		static void SetAvatarTypeRokona() {
			CurrentAvatarType = AvatarType.Rokona;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Rue", priority = 1100)]
		static void SetAvatarTypeRue() {
			CurrentAvatarType = AvatarType.Rue;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Rurune", priority = 1100)]
		static void SetAvatarTypeRurune() {
			CurrentAvatarType = AvatarType.Rurune;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Rusk", priority = 1100)]
		static void SetAvatarTypeRusk() {
			CurrentAvatarType = AvatarType.Rusk;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/SELESTIA", priority = 1100)]
		static void SetAvatarTypeSELESTIA() {
			CurrentAvatarType = AvatarType.SELESTIA;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Sephira", priority = 1100)]
		static void SetAvatarTypeSephira() {
			CurrentAvatarType = AvatarType.Sephira;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Shami", priority = 1100)]
		static void SetAvatarTypeShami() {
			CurrentAvatarType = AvatarType.Shami;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Shinano", priority = 1100)]
		static void SetAvatarTypeShinano() {
			CurrentAvatarType = AvatarType.Shinano;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Shinra", priority = 1100)]
		static void SetAvatarTypeShinra() {
			CurrentAvatarType = AvatarType.Shinra;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/SHIRAHA", priority = 1100)]
		static void SetAvatarTypeSHIRAHA() {
			CurrentAvatarType = AvatarType.SHIRAHA;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Shiratsume", priority = 1100)]
		static void SetAvatarTypeShiratsume() {
			CurrentAvatarType = AvatarType.Shiratsume;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Sio", priority = 1100)]
		static void SetAvatarTypeSio() {
			CurrentAvatarType = AvatarType.Sio;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Sue", priority = 1100)]
		static void SetAvatarTypeSue() {
			CurrentAvatarType = AvatarType.Sue;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Sugar", priority = 1100)]
		static void SetAvatarTypeSugar() {
			CurrentAvatarType = AvatarType.Sugar;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Suzuhana", priority = 1100)]
		static void SetAvatarTypeSuzuhana() {
			CurrentAvatarType = AvatarType.Suzuhana;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Tien", priority = 1100)]
		static void SetAvatarTypeTien() {
			CurrentAvatarType = AvatarType.Tien;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/TubeRose", priority = 1100)]
		static void SetAvatarTypeTubeRose() {
			CurrentAvatarType = AvatarType.TubeRose;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Ukon", priority = 1100)]
		static void SetAvatarTypeUkon() {
			CurrentAvatarType = AvatarType.Ukon;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Usasaki", priority = 1100)]
		static void SetAvatarTypeUsasaki() {
			CurrentAvatarType = AvatarType.Usasaki;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Uzuki", priority = 1100)]
		static void SetAvatarTypeUzuki() {
			CurrentAvatarType = AvatarType.Uzuki;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/VIVH", priority = 1100)]
		static void SetAvatarTypeVIVH() {
			CurrentAvatarType = AvatarType.VIVH;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Wolferia", priority = 1100)]
		static void SetAvatarTypeWolferia() {
			CurrentAvatarType = AvatarType.Wolferia;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Yoll", priority = 1100)]
		static void SetAvatarTypeYoll() {
			CurrentAvatarType = AvatarType.Yoll;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/YUGI MIYO", priority = 1100)]
		static void SetAvatarTypeYUGI_MIYO() {
			CurrentAvatarType = AvatarType.YUGI_MIYO;
			CheckAvatarMenu();
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/Avatar/Yuuko", priority = 1100)]
		static void SetAvatarTypeYuuko() {
			CurrentAvatarType = AvatarType.Yuuko;
			CheckAvatarMenu();
		}
		// 검색용 신규 아바타 추가 위치

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/100cm", priority = 1100)]
		static void ScaleAvatar100cm() {
			RequestScaleAvatar(100);
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/110cm", priority = 1100)]
		static void ScaleAvatar110cm() {
			RequestScaleAvatar(110);
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/120cm", priority = 1100)]
		static void ScaleAvatar120cm() {
			RequestScaleAvatar(120);
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/130cm", priority = 1100)]
		static void ScaleAvatar130cm() {
			RequestScaleAvatar(130);
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/140cm", priority = 1100)]
		static void ScaleAvatar140cm() {
			RequestScaleAvatar(140);
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/150cm", priority = 1100)]
		static void ScaleAvatar150cm() {
			RequestScaleAvatar(150);
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/160cm", priority = 1100)]
		static void ScaleAvatar160cm() {
			RequestScaleAvatar(160);
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/170cm", priority = 1100)]
		static void ScaleAvatar170cm() {
			RequestScaleAvatar(170);
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/180cm", priority = 1100)]
		static void ScaleAvatar180cm() {
			RequestScaleAvatar(180);
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/190cm", priority = 1100)]
		static void ScaleAvatar190cm() {
			RequestScaleAvatar(190);
		}

		[MenuItem("Tools/VRSuya/Utility/AvatarScaler/200cm", priority = 1100)]
		static void ScaleAvatar200cm() {
			RequestScaleAvatar(200);
		}

		internal static void ScaleAvatarHeight(int TargetAvatarHeight) {
			RequestScaleAvatar(TargetAvatarHeight);
		}

		static AvatarScaler() {
			CheckAvatarMenu();
		}

		static void CheckAvatarMenu() {
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Automatic Avatar Recognition", AutomaticAvatarRecognition);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Airi", CurrentAvatarType == AvatarType.Airi);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Aldina", CurrentAvatarType == AvatarType.Aldina);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Angura", CurrentAvatarType == AvatarType.Angura);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Anon", CurrentAvatarType == AvatarType.Anon);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Anri", CurrentAvatarType == AvatarType.Anri);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Ash", CurrentAvatarType == AvatarType.Ash);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Chiffon", CurrentAvatarType == AvatarType.Chiffon);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Chise", CurrentAvatarType == AvatarType.Chise);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Chocolat", CurrentAvatarType == AvatarType.Chocolat);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Cygnet", CurrentAvatarType == AvatarType.Cygnet);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Eku", CurrentAvatarType == AvatarType.Eku);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Emmelie", CurrentAvatarType == AvatarType.Emmelie);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/EYO", CurrentAvatarType == AvatarType.EYO);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Firina", CurrentAvatarType == AvatarType.Firina);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Flare", CurrentAvatarType == AvatarType.Flare);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Fuzzy", CurrentAvatarType == AvatarType.Fuzzy);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Glaze", CurrentAvatarType == AvatarType.Glaze);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Grus", CurrentAvatarType == AvatarType.Grus);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Hakka", CurrentAvatarType == AvatarType.Hakka);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/IMERIS", CurrentAvatarType == AvatarType.IMERIS);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Karin", CurrentAvatarType == AvatarType.Karin);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Kikyo", CurrentAvatarType == AvatarType.Kikyo);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Kipfel", CurrentAvatarType == AvatarType.Kipfel);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Kokoa", CurrentAvatarType == AvatarType.Kokoa);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Koyuki", CurrentAvatarType == AvatarType.Koyuki);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/KUMALY", CurrentAvatarType == AvatarType.KUMALY);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Kuronatu", CurrentAvatarType == AvatarType.Kuronatu);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Lapwing", CurrentAvatarType == AvatarType.Lapwing);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Lazuli", CurrentAvatarType == AvatarType.Lazuli);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Leefa", CurrentAvatarType == AvatarType.Leefa);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Leeme", CurrentAvatarType == AvatarType.Leeme);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Lime", CurrentAvatarType == AvatarType.Lime);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/LUMINA", CurrentAvatarType == AvatarType.LUMINA);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Lunalitt", CurrentAvatarType == AvatarType.Lunalitt);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Mafuyu", CurrentAvatarType == AvatarType.Mafuyu);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Maki", CurrentAvatarType == AvatarType.Maki);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Mamehinata", CurrentAvatarType == AvatarType.Mamehinata);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/MANUKA", CurrentAvatarType == AvatarType.MANUKA);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Mariel", CurrentAvatarType == AvatarType.Mariel);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Marron", CurrentAvatarType == AvatarType.Marron);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Maya", CurrentAvatarType == AvatarType.Maya);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/MAYO", CurrentAvatarType == AvatarType.MAYO);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Merino", CurrentAvatarType == AvatarType.Merino);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Miko", CurrentAvatarType == AvatarType.Miko);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Milfy", CurrentAvatarType == AvatarType.Milfy);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Milk", CurrentAvatarType == AvatarType.Milk);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Milltina", CurrentAvatarType == AvatarType.Milltina);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Minahoshi", CurrentAvatarType == AvatarType.Minahoshi);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Minase", CurrentAvatarType == AvatarType.Minase);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Mint", CurrentAvatarType == AvatarType.Mint);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Mir", CurrentAvatarType == AvatarType.Mir);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Misaki", CurrentAvatarType == AvatarType.Misaki);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Mishe", CurrentAvatarType == AvatarType.Mishe);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Moe", CurrentAvatarType == AvatarType.Moe);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Nayu", CurrentAvatarType == AvatarType.Nayu);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Nehail", CurrentAvatarType == AvatarType.Nehail);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Nochica", CurrentAvatarType == AvatarType.Nochica);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Platinum", CurrentAvatarType == AvatarType.Platinum);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Plum", CurrentAvatarType == AvatarType.Plum);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Pochimaru", CurrentAvatarType == AvatarType.Pochimaru);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Quiche", CurrentAvatarType == AvatarType.Quiche);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Rainy", CurrentAvatarType == AvatarType.Rainy);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Ramune", CurrentAvatarType == AvatarType.Ramune);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Ramune(Old)", CurrentAvatarType == AvatarType.Ramune_Old);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/RINDO", CurrentAvatarType == AvatarType.RINDO);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Rokona", CurrentAvatarType == AvatarType.Rokona);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Rue", CurrentAvatarType == AvatarType.Rue);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Rurune", CurrentAvatarType == AvatarType.Rurune);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Rusk", CurrentAvatarType == AvatarType.Rusk);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/SELESTIA", CurrentAvatarType == AvatarType.SELESTIA);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Sephira", CurrentAvatarType == AvatarType.Sephira);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Shami", CurrentAvatarType == AvatarType.Shami);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Shinano", CurrentAvatarType == AvatarType.Shinano);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Shinra", CurrentAvatarType == AvatarType.Shinra);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/SHIRAHA", CurrentAvatarType == AvatarType.SHIRAHA);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Shiratsume", CurrentAvatarType == AvatarType.Shiratsume);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Sio", CurrentAvatarType == AvatarType.Sio);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Sue", CurrentAvatarType == AvatarType.Sue);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Sugar", CurrentAvatarType == AvatarType.Sugar);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Suzuhana", CurrentAvatarType == AvatarType.Suzuhana);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Tien", CurrentAvatarType == AvatarType.Tien);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/TubeRose", CurrentAvatarType == AvatarType.TubeRose);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Ukon", CurrentAvatarType == AvatarType.Ukon);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Usasaki", CurrentAvatarType == AvatarType.Usasaki);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Uzuki", CurrentAvatarType == AvatarType.Uzuki);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/VIVH", CurrentAvatarType == AvatarType.VIVH);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Wolferia", CurrentAvatarType == AvatarType.Wolferia);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Yoll", CurrentAvatarType == AvatarType.Yoll);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/YUGI MIYO", CurrentAvatarType == AvatarType.YUGI_MIYO);
			Menu.SetChecked("Tools/VRSuya/Utility/AvatarScaler/Avatar/Yuuko", CurrentAvatarType == AvatarType.Yuuko);
			// 검색용 신규 아바타 추가 위치
		}

		public static void RequestScaleAvatar(int TargetHeight) {
			GameObject[] TargetAvatarGameObjects = Selection.gameObjects;
			if (TargetAvatarGameObjects.Length == 0) {
				TargetAvatarGameObjects = AvatarUtility.GetAvatarGameObjects();
			}
			if (TargetAvatarGameObjects.Length > 0) {
				UndoGroupIndex = UnityUtility.InitializeUndoGroup(UndoGroupName);
				foreach (GameObject TargetAvatarGameObject in TargetAvatarGameObjects) {
					ScaleAvatar(TargetAvatarGameObject, TargetHeight);
				}
				SceneView.RepaintAll();
			}
		}

		public static bool ScaleAvatar(GameObject TargetAvatarGameObject, int TargetHeight) {
			if (!TargetAvatarGameObject) return false;
			VRC_AvatarDescriptor TargetAvatarDescriptor = TargetAvatarGameObject.GetComponent<VRC_AvatarDescriptor>();
			if (!TargetAvatarDescriptor) return false;
			Vector3 AvatarViewPosition = TargetAvatarDescriptor.ViewPosition;
			AvatarType NewAvatarType = (AutomaticAvatarRecognition) ? GetAvatarType(TargetAvatarGameObject) : CurrentAvatarType;
			float TargetEyeHeight = AvatarEyeHeights[NewAvatarType] * TargetHeight / 100;
			float TargetAvatarScale = TargetEyeHeight / AvatarViewPosition.y;
			ScaleAvatarTransform(TargetAvatarGameObject, TargetAvatarScale);
			ScaleAvatarViewPosition(TargetAvatarDescriptor, TargetAvatarScale);
			Debug.Log($"[VRSuya] Set the height of {TargetAvatarGameObject.name} avatar to {TargetHeight}cm");
			return true;
		}

		static AvatarType GetAvatarType(GameObject TargetGameObject) {
			AvatarType NewAvatarType = CurrentAvatarType;
			foreach (var TargetAvatarNameKeyPair in AvatarNameDictionary) {
				foreach (string TranslatedName in TargetAvatarNameKeyPair.Value) {
					if (TargetGameObject.name.Contains(TranslatedName, StringComparison.OrdinalIgnoreCase)) {
						return TargetAvatarNameKeyPair.Key;
					}
				}
			}
			return NewAvatarType;
		}

		static void ScaleAvatarTransform(GameObject TargetAvatar, float TargetScale) {
			Transform TargetAvatarTransform = TargetAvatar.transform;
			Undo.RecordObject(TargetAvatarTransform, UndoGroupName);
			TargetAvatarTransform.localScale = TargetAvatarTransform.localScale * TargetScale;
			EditorUtility.SetDirty(TargetAvatarTransform);
			Undo.CollapseUndoOperations(UndoGroupIndex);
		}

		static void ScaleAvatarViewPosition(VRC_AvatarDescriptor TargetAvatarDescriptor, float TargetScale) {
			Undo.RecordObject(TargetAvatarDescriptor, UndoGroupName);
			TargetAvatarDescriptor.ViewPosition = TargetAvatarDescriptor.ViewPosition * TargetScale;
			EditorUtility.SetDirty(TargetAvatarDescriptor);
			Undo.CollapseUndoOperations(UndoGroupIndex);
		}
	}

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
			GUILayout.Label(GetTranslatedString("String_AvatarHeight"), EditorStyles.boldLabel);
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
			if (GUILayout.Button(GetTranslatedString("String_Apply"), GUILayout.Width(100))) {
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