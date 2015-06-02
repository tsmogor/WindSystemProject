using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Answers
{
	public Player player;
	public List<Answer> answers;
	
	
	public Answers(Player player,params string[] answers)
	{
		this.answers = new List<Answer>();
		this.player = player;
		foreach(var i in answers)
		{
			this.answers.Add(new Answer(i,0));
		}
	}
}

public class Answer
{
	public string answer;
	public int points;
	
	public Answer(string answer, int points)
	{
		this.answer = answer;
		this.points = points;
	}
}

public class Round
{
	public char letter;
	bool isFinished = false;
	public List<Answers> playersAnswers;
	
	public Round(char a) // for testing
	{
		letter = a;
		playersAnswers = new List<Answers>();
	}
	
	
}

public class CountryScript : MonoBehaviour 
{
	
	
	private List<GameObject> inputFields;
	public List<Round> rounds = new List<Round>();
	private string alphabet = "ABCDEFGHIJKLMNOPRSTUWZ";
	public List<string> country;
	public List<string> town;
	public List<string> animal;
	public List<string> name;
	
	public int score;
	
	public char RandomLetter()
	{
		// return [A-Z]
		
		if (alphabet.Length == 0)
		{
			alphabet = "ABCDEFGHIJKLMNOPRSTUWZ";
		}
		int number = Random.Range(0, alphabet.Length); // Zero to 22
		char letter = alphabet[number];
		alphabet.Remove(number);
		return letter;
	}
	
	public List<Round> getRounds()
	{
		return rounds;
	}
	// Use this for initialization
	void Start () 
	{
		Network.maxConnections = -1;
		score = 0;
		country = new List<string>() { "ABCHAZJA","AFGANISTAN","ALBANIA","ALGIERIA","ANDORA","ANGOLA","ANTIGUA I BARBUDA","ARABIA SAUDYJSKA","ARGENTYNA","ARMENIA","AUSTRALIA","AUSTRIA","AZERBEJDŻAN","BAHAMY","BAHRAJN","BANGLADESZ","BARBADOS","BELGIA","BELIZE","BENIN","BHUTAN","BIAŁORUŚ","BIRMA","BOLIWIA","BOŚNIA I HERCEGOWINA","BOTSWANA","BRAZYLIA","BRUNEI","BUŁGARIA","BURKINA FASO","BURUNDI","CHILE","CHINY","CHORWACJA","CYPR","CYPR PÓŁNOCNY","CZAD","CZARNOGÓRA","CZECHY","DANIA","DEMOKRATYCZNA REPUBLIKA KONGA","DOMINIKA","DOMINIKANA","DŻIBUTI","EGIPT","EKWADOR","ERYTREA","ESTONIA","ETIOPIA","FIDŻI","FILIPINY","FINLANDIA","FRANCJA","GABON","GAMBIA","GHANA","GÓRSKI KARABACH","GRECJA","GRENADA","GRUZJA","GUJANA","GWATEMALA","GWINEA","GWINEA BISSAU","GWINEA RÓWNIKOWA","HAITI","HISZPANIA","HOLANDIA","HONDURAS","INDIE","INDONEZJA","IRAK","IRAN","IRLANDIA","ISLANDIA","IZRAEL","JAMAJKA","JAPONIA","JEMEN","JORDANIA","KAMBODŻA","KAMERUN","KANADA","KATAR","KAZACHSTAN","KENIA","KIRGISTAN","KIRIBATI","KOLUMBIA","KOMORY","KONGO","KOREA POŁUDNIOWA","KOREA PÓŁNOCNA","KOSOWO","KOSTARYKA","KUBA","KUWEJT","LAOS","LESOTHO","LIBAN","LIBERIA","LIBIA","LIECHTENSTEIN","LITWA","LUKSEMBURG","ŁOTWA","MACEDONIA","MADAGASKAR","MALAWI","MALEDIWY","MALEZJA","MALI","MALTA","MAROKO","MAURETANIA","MAURITIUS","MEKSYK","MIKRONEZJA","MOŁDAWIA","MONAKO","MONGOLIA","MOZAMBIK","NADDNIESTRZE","NAMIBIA","NAURU","NEPAL","NIEMCY","NIGER","NIGERIA","NIKARAGUA","NORWEGIA","NOWA ZELANDIA","OMAN","OSETIA POŁUDNIOWA","PAKISTAN","PANAMA","PAPUA - NOWA GWINEA","PARAGWAJ","PERU","POLSKA","PORTUGALIA","REPUBLIKA POŁUDNIOWEJ AFRYKI","REPUBLIKA ŚRODKOWOAFRYKAŃSKA","REPUBLIKA ZIELONEGO PRZYLĄDKA","ROSJA","RUMUNIA","RWANDA","SAHARA ZACHODNIA","SAINT KITTS I NEVIS","SAINT LUCIA","SAINT VINCENT I GRENADYNY","SALWADOR","SAMOA","SAN MARINO","SENEGAL","SERBIA","SESZELE","SIERRA LEONE","SINGAPUR","SŁOWACJA","SŁOWENIA","SOMALIA","SOMALILAND","SRI LANKA","STANY ZJEDNOCZONE","SUAZI","SUDAN","SUDAN POŁUDNIOWY","SURINAM","SYRIA","SZWAJCARIA","SZWECJA","TADŻYKISTAN","TAJLANDIA","TAJWAN","TANZANIA","TIMOR WSCHODNI","TOGO","TONGA","TRYNIDAD I TOBAGO","TUNEZJA","TURCJA","TURKMENISTAN","TUVALU","UGANDA","UKRAINA","URUGWAJ","UZBEKISTAN","VANUATU","WATYKAN","WENEZUELA","WĘGRY","WIELKA BRYTANIA","WIETNAM","WŁOCHY","WYBRZEŻE KOŚCI SŁONIOWEJ","WYSPY SALOMONA","WYSPY ŚWIĘTEGO TOMASZA I KSIĄŻĘCA","ZAMBIA","ZIMBABWE","ZJEDNOCZONE EMIRATY ARABSKIE" };
		town = new List<string>() { "ALWERNIA","ANDRYCHÓW","ANNOPOL","AUGUSTÓW","BABIMOST","BABORÓW","BARCIN","BARCZEWO","BARLINEK","BARTOSZYCE","BARWICE","BEŁCHATÓW","BEŁŻYCE","BĘDZIN","BIAŁOBRZEGI","BIAŁOGARD","BIAŁYSTOK","BIECZ","BIELAWA","BIELSKO-BIAŁA","BIERUŃ","BIERUTÓW","BIEŻUŃ","BIŁGORAJ","BISKUPIEC","BISZTYNEK","BLACHOWNIA","BŁASZKI","BŁAŻOWA","BŁONIE","BOBOLICE","BOCHNIA","BODZENTYN","BOGATYNIA","BOGUSZÓW-GORCE","BOJANOWO","BOLESŁAWIEC","BOLKÓW","BRANIEWO","BRAŃSK","BRODNICA","BROK","BRUSY","BRWINÓW","BRZESKO","BRZESZCZE","BRZEZINY","BRZOZÓW","BUKOWNO","BYCHAWA","BYCZYNA","BYDGOSZCZ","BYTOM","BYTÓW","CEDYNIA","CHEŁM","CHEŁMEK","CHEŁMNO","CHEŁMŻA","CHĘCINY","CHOCIANÓW","CHOCIWEL","CHODECZ","CHODZIEŻ","CHOJNA","CHOJNICE","CHOJNÓW","CHOROSZCZ","CHORZELE","CHORZÓW","CHOSZCZNO","CHRZANÓW","CIECHANOWIEC","CIECHANÓW","CIECHOCINEK","CIESZANÓW","CIESZYN","CIĘŻKOWICE","CYBINKA","CZAPLINEK","CZARNE","CZARNKÓW","CZCHÓW","CZECHOWICE-DZIEDZICE","CZELADŹ","CZEMPIŃ","CZERNIEJEWO","CZERSK","CZERWIEŃSK","CZERWIONKA-LESZCZYNY","CZĘSTOCHOWA","CZŁOPA","CZŁUCHÓW","ĆMIELÓW","DARŁOWO","DĄBIE","DEBRZNO","DĘBICA","DĘBLIN","DĘBNO","DOBCZYCE","DOBIEGNIEW","DOBRODZIEŃ","DOBRZANY","DOLSK","DRAWNO","DREZDENKO","DROBIN","DUKLA","DYNÓW","DZIAŁDOWO","DZIAŁOSZYCE","DZIAŁOSZYN","DZIERZGOŃ","DZIERŻONIÓW","ELBLĄG","EŁK","FRAMPOL","FROMBORK","GARWOLIN","GĄBIN","GDAŃSK","GDYNIA","GIŻYCKO","GLINOJECK","GLIWICE","GŁOGÓW","GŁOGÓWEK","GŁOWNO","GŁUBCZYCE","GŁUCHOŁAZY","GŁUSZYCA","GNIEWKOWO","GNIEZNO","GOGOLIN","GOLCZEWO","GOLENIÓW","GOLINA","GOLUB-DOBRZYŃ","GOŁAŃCZ","GOŁDAP","GONIĄDZ","GORLICE","GOSTYNIN","GOSTYŃ","GOZDNICA","GÓRZNO","GRAJEWO","GRODKÓW","GRÓJEC","GRUDZIĄDZ","GRYBÓW","GRYFICE","GRYFINO","GUBIN","HAJNÓWKA","HRUBIESZÓW","IŁAWA","IŁOWA","IŁŻA","IMIELIN","INOWROCŁAW","IŃSKO","JANIKOWO","JAROCIN","JAROSŁAW","JASŁO","JASTARNIA","JASTROWIE","JAWORZNO","JEDLICZE","JEDWABNE","JELCZ-LASKOWICE","JEZIORANY","JĘDRZEJÓW","JORDANÓW","JUTROSIN","KALETY","KALISZ","KAŁUSZYN","KAMIEŃSK","KAŃCZUGA","KARCZEW","KARGOWA","KARLINO","KARPACZ","KARTUZY","KATOWICE","KCYNIA","KĘDZIERZYN-KOŹLE","KĘPICE","KĘPNO","KĘTRZYN","KĘTY","KIELCE","KIETRZ","KISIELICE","KLECZEW","KLESZCZELE","KLUCZBORK","KŁECKO","KŁOBUCK","KŁODAWA","KŁODZKO","KNURÓW","KNYSZYN","KOBYLIN","KOBYŁKA","KOCK","KOLBUSZOWA","KOLNO","KOLONOWSKIE","KOLUSZKI","KOŁOBRZEG","KONIECPOL","KONIN","KONSTANCIN-JEZIORNA","KOŃSKIE","KORFANTÓW","KORONOWO","KORSZE","KOSTRZYN","KOSTRZYN","KOSZALIN","KOŚCIAN","KOŚCIERZYNA","KOWARY","KOZIENICE","KOŻUCHÓW","KÓRNIK","KRAJENKA","KRAKÓW","KRAPKOWICE","KRASNOBRÓD","KRASNYSTAW","KRAŚNIK","KROBIA","KROSNO","KROŚNIEWICE","KROTOSZYN","KRUSZWICA","KRZEPICE","KRZESZOWICE","KRZYWIŃ","KUNÓW","KUTNO","KWIDZYN","LEGIONOWO","LEGNICA","LESKO","LESZNO","LEŚNA","LEŚNICA","LEŻAJSK","LĘBORK","LĘDZINY","LIBIĄŻ","LIDZBARK","LIMANOWA","LIPIANY","LIPNO","LIPSK","LIPSKO","LUBACZÓW","LUBARTÓW","LUBAWA","LUBAWKA","LUBIN","LUBLIN","LUBLINIEC","LUBNIEWICE","LUBOMIERZ","LUBOŃ","LUBRANIEC","LUBSKO","LWÓWEK","ŁABISZYN","ŁAŃCUT","ŁAPY","ŁASIN","ŁASK","ŁASKARZEW","ŁAZY","ŁEBA","ŁĘCZNA","ŁĘCZYCA","ŁĘKNICA","ŁOBEZ","ŁOBŻENICA","ŁOCHÓW","ŁOMIANKI","ŁOMŻA","ŁOSICE","ŁOWICZ","ŁÓDŹ","ŁUKÓW","MALBORK","MAŁOGOSZCZ","MAŁOMICE","MARGONIN","MARKI","MASZEWO","MIASTKO","MIECHÓW","MIELEC","MIEROSZÓW","MIESZKOWICE","MIĘDZYBÓRZ","MIĘDZYCHÓD","MIĘDZYLESIE","MIĘDZYLESIE","MIĘDZYRZECZ","MIĘDZYZDROJE","MIKOŁAJKI","MIKOŁÓW","MIKSTAT","MILANÓWEK","MILICZ","MIŁAKOWO","MIŁOMŁYN","MIŁOSŁAW","MIROSŁAWIEC","MIRSK","MŁAWA","MŁYNARY","MOGIELNICA","MOGILNO","MOŃKI","MORĄG","MORDY","MORYŃ","MOSINA","MRĄGOWO","MROCZA","MSZCZONÓW","MUSZYNA","MYSŁOWICE","MYSZKÓW","MYSZYNIEC","MYŚLENICE","MYŚLIBÓRZ","NAŁĘCZÓW","NAMYSŁÓW","NAROL","NASIELSK","NEKLA","NIDZICA","NIEMCZA","NIEMODLIN","NIEPOŁOMICE","NIESZAWA","NISKO","NOWE","NOWOGRODZIEC","NYSA","OBORNIKI","OBRZYCKO","ODOLANÓW","OGRODZIENIEC","OKONEK","OLECKO","OLESNO","OLESZYCE","OLEŚNICA","OLKUSZ","OLSZTYN","OLSZTYNEK","OŁAWA","OPALENICA","OPATÓW","OPOCZNO","OPOLE","ORNETA","ORZESZE","ORZYSZ","OSIECZNA","OSTROŁĘKA","OSTRORÓG","OSTRÓDA","OSTRZESZÓW","OSTRZESZÓW","OŚWIĘCIM","OTMUCHÓW","OTWOCK","OZIMEK","OZORKÓW","OŻARÓW","PABIANICE","PACZKÓW","PAJĘCZNO","PAKOŚĆ","PARCZEW","PASŁĘK","PASYM","PELPLIN","PEŁCZYCE","PIASECZNO","PIASTÓW","PIECHOWICE","PIENIĘŻNO","PIEŃSK","PIESZYCE","PILICA","PILZNO","PIŃCZÓW","PIONKI","PISZ","PLESZEW","PŁOCK","PŁOŃSK","PŁOTY","PNIEWY","POBIEDZISKA","PODDĘBICE","POGORZELA","POLANÓW","POLICE","POLKOWICE","POŁANIEC","PONIATOWA","PONIEC","PORĘBA","POZNAŃ","PRABUTY","PRASZKA","PROCHOWICE","PROSZOWICE","PRUDNIK","PRUSICE","PRUSZKÓW","PRZASNYSZ","PRZEDBÓRZ","PRZEDECZ","PRZEMKÓW","PRZEMYŚL","PRZEWORSK","PRZYSUCHA","PSZCZYNA","PSZÓW","PUCK","PUŁAWY","PUŁTUSK","PUSZCZYKOWO","PYRZYCE","PYSKOWICE","PYZDRY","RABKA","RACIĄŻ","RACIBÓRZ","RADKÓW","RADLIN","RADOM","RADOMSKO","RADYMNO","RADZIEJÓW","RADZIONKÓW","RADZYMIN","RAJGRÓD","RAKONIEWICE","RASZKÓW","RAWICZ","RECZ","RESKO","RESZEL","ROGOŹNO","ROPCZYCE","RÓŻAN","RUCIANE-NIDA","RUMIA","RYBNIK","RYCHWAŁ","RYDUŁTOWY","RYDZYNA","RYKI","RYMANÓW","RYN","RYPIN","RZEPIN","RZESZÓW","SANDOMIERZ","SANOK","SEJNY","SEROCK","SĘDZISZÓW","SĘPOPOL","SIANÓW","SIECHNICE","SIEDLCE","SIEMIATYCZE","SIENIAWA","SIERADZ","SIERAKÓW","SIERPC","SIEWIERZ","SKALBMIERZ","SKARSZEWY","SKARŻYSKO-KAMIENNA","SKAWINA","SKĘPE","SKIERNIEWICE","SKOCZÓW","SKÓRCZ","SKWIERZYNA","SŁAWKÓW","SŁAWNO","SŁOMNIKI","SŁUBICE","SŁUPCA","SŁUPSK","SOBÓTKA","SOCHACZEW","SOKÓŁKA","SOMPOLNO","SOPOT","SOSNOWIEC","SOŚNICOWICE","STARACHOWICE","STASZÓW","STAWISKI","STAWISZYN","STĄPORKÓW","STĘSZEW","STRYKÓW","STRZEGOM","STRZELIN","STRZELNO","STRZYŻÓW","SUCHAŃ","SUCHEDNIÓW","SUCHOWOLA","SULECHÓW","SULEJÓW","SULEJÓWEK","SULĘCIN","SULMIERZYCE","SUŁKOWICE","SUPRAŚL","SURAŻ","SUSZ","SUSZ","SUWAŁKI","SWARZĘDZ","SYCÓW","SZADEK","SZAMOCIN","SZAMOTUŁY","SZCZAWNICA","SZCZEBRZESZYN","SZCZECIN","SZCZECINEK","SZCZEKOCINY","SZCZYRK","SZCZYTNA","SZCZYTNO","SZLICHTYNGOWA","SZPROTAWA","SZTUM","SZUBIN","SZYDŁOWIEC","ŚCINAWA","ŚLESIN","ŚMIGIEL","ŚREM","ŚWIDNICA","ŚWIDNIK","ŚWIDWIN","ŚWIEBODZICE","ŚWIEBODZIN","ŚWIECIE","ŚWIERZAWA","ŚWIĘTOCHŁOWICE","ŚWINOUJŚCIE","TARNOBRZEG","TARNOGRÓD","TARNÓW","TCZEW","TERESPOL","TOLKMICKO","TORUŃ","TORZYM","TOSZEK","TRZCIANKA","TRZCIEL","TRZEBIATÓW","TRZEBINIA","TRZEBNICA","TRZEMESZNO","TUCHOLA","TUCHÓW","TUCZNO","TULISZKÓW","TUREK","TUSZYN","TWARDOGÓRA","TYCHY","TYCZYN","TYKOCIN","UJAZD","ULANÓW","UNIEJÓW","USTKA","USTROŃ","WADOWICE","WAŁBRZYCH","WAŁCZ","WARKA","WARSZAWA","WARTA","WASILKÓW","WĄBRZEŹNO","WĄCHOCK","WĄGROWIEC","WEJHEROWO","WĘGLINIEC","WĘGORZEWO","WĘGORZYNO","WĘGRÓW","WIĄZÓW","WIELEŃ","WIELICHOWO","WIELICZKA","WIELUŃ","WIERUSZÓW","WIĘCBORK","WILAMOWICE","WISŁA","WITKOWO","WITNICA","WLEŃ","WŁADYSŁAWOWO","WŁOCŁAWEK","WŁODAWA","WŁOSZCZOWA","WOJCIESZÓW","WOJKOWICE","WOLBROM","WOLSZTYN","WOŁCZYN","WOŁOMIN","WOŁÓW","WOŹNIKI","WROCŁAW","WRONKI","WRZEŚNIA","WSCHOWA","WYRZYSK","WYSZKÓW","WYSZOGRÓD","WYŚMIERZYCE","ZABŁUDÓW","ZABRZE","ZAGÓRÓW","ZAGÓRZ","ZAKOPANE","ZAKROCZYM","ZALEWO","ZAMBRÓW","ZAMOŚĆ","ZAWADZKIE","ZAWICHOST","ZAWIDÓW","ZAWIERCIE","ZĄBKI","ZBĄSZYNEK","ZBĄSZYŃ","ZDUNY","ZDZIESZOWICE","ZELÓW","ZGIERZ","ZGORZELEC","ZIELONKA","ZIĘBICE","ZŁOCIENIEC","ZŁOCZEW","ZŁOTORYJA","ZŁOTÓW","ZWOLEŃ","ŻABNO","ŻAGAŃ","ŻARKI","ŻARÓW","ŻARY","ŻELECHÓW","ŻERKÓW","ŻMIGRÓD","ŻNIN","ŻORY","ŻUKOWO","ŻUROMIN","ŻYCHLIN","ŻYRARDÓW","ŻYWIEC"};
		animal = new List<string>() { "ANAKONDA", "ANTYLOPA", "ARA", "ALIGATOR", "BOCIAN", "BYK", "BARAN", "BAŻANT", "BÓBR", "BORSUK", "BIZON", "CZAPLA", "CHRABĄSZCZ", "CHOMIK", "ĆMA", "DZIK", "DELFIN", "DIK-DIK", "DZIĘCIOŁ", "DROMADER", "EMU", "FOKA", "FLAMING", "GORYL", "GAWRON", "GEPARD", "GĘŚ", "GNU", "GOŁĄB", "HIENA", "HIPOPOTAM", "INDYK", "IBIS", "JAGUAR", "JAK", "JASKÓŁKA", "JELEŃ", "JEŻ", "KOMAR", "KRÓLIK", "KOT", "KURA", "KOALA", "KUKUŁKA", "KORNIK", "KANGUR", "KROWA", "KOŃ", "LIS", "LEW", "LAMA", "LAMPART", "ŁOSOŚ", "ŁABĘDŹ", "ŁOŚ", "MUCHA", "MYSZ", "MRÓWKA", "MAŁPA", "MEWA", "MOTYL", "MRÓWKA", "MORS", "NIEDŹWIEDŹ", "NOSOROŻEC", "NIETOPERZ", "OWCA", "OSA", "ORKA", "ORZEŁ", "OSIOŁ", "OŚMIORNICA", "PSZCZOŁA", "PIES", "PANGA", "PANDA", "PATYCZAK", "PAJĄK", "PELIKAN", "PAW", "PINGWIN", "PSTRĄG", "RAK", "RYBA", "REKIN", "RENIFER", "RYŚ", "STRUŚ", "SARNA", "SŁOŃ", "SURYKATKA", "SĘP", "SOWA", "SZCZUR", "SZPAK", "SZYMPANS", "ŚLIMAK", "ŚWINIA", "ŚWINKAMORSKA", "ŚWISTAK", "TERMIT", "TRZMIEL", "TYGRYS", "UCHATKA", "UKWIAŁ", "WILK", "WIELORYB", "WAŻKA", "WIELBŁĄD", "WIEWIÓRKA", "WRÓBEL", "WYDRA", "ZAJĄC", "ZEBRA", "ZIMORODEK", "ŻUK", "ŻYRAFA", "ŻABA", "ŻMIJA", "ŻÓŁW", "ŻURAW", "ŹREBAK", };
		name = new List<string>() { "ADAM", "ADRIAN", "ADOLF", "ALBERT", "ALBIN", "ALEKSANDER", "ALEKSY", "ALFONS", "ALFRED", "ALOJZY", "AMBROŻY", "ANATOL", "ANDRZEJ", "ANTONI", "ANZELM", "APOLINARY", "AURELIUSZ", "ARKADIUSZ", "ARTUR", "AUGUSTYN", "BARTOSZ", "BOGDAN", "BOGUSŁAW", "BOLESŁAW", "CEZARY", "CYPRIAN", "CZESŁAW", "DAMIAN", "DANIEL", "DARIUSZ", "DAWID", "DIONIZY", "DOMINIK", "EDWARD", "EMIL", "ERNEST", "ERYK", "FELIKS", "FILIP", "FRYDERYK", "GRZEGORZ", "GWIDON", "HENRYK", "HERBERT", "IRENEUSZ", "JACEK", "JAKUB", "JAN", "JAREK", "JERZY", "JULIUSZ", "KAJETAN", "KAMIL", "KACPER", "KAROL", "KAZIMIERZ", "KONRAD", "KRYSTIAN", "KRZYSZTOF", "LECH", "LEOPOLD", "LESZEK", "LUCJAN", "ŁUKASZ", "MACIEJ", "MAKSYMILIAN", "MARCIN", "MAREK", "MARIUSZ", "MATEUSZ", "MICHAŁ", "MIKOŁAJ", "MIROSŁAW", "NORBERT", "OLGIERD", "OSKAR", "PAWEŁ", "PATRYK", "PIOTR", "PRZEMYSŁAW", "RADOSŁAW", "RAFAŁ", "REMIGIUSZ", "ROBERT", "ROMAN", "SEBASTIAN", "SŁAWOMIR", "STANISŁAW", "STEFAN", "SYLWEK", "SZYMON", "TADEUSZ", "TOMASZ", "WACŁAW", "WALDEMAR", "WAWRZYNIEC", "WIKTOR", "WINCENTY", "WIT", "WITOLD", "WŁADYSŁAW", "WŁODZIMIERZ", "WOJCIECH", "ZBIGNIEW", "ZBYSZEK", "ZDZISŁAW", "ZENOBIUSZ", "ZENON", "ZYGMUNT", "ADA", "ADELA", "AGATA", "AGNIESZKA", "ALDONA", "ALEKSANDRA", "ALICJA", "ALINA", "ANASTAZJA", "ANETA", "ANGELIKA", "ANIELA", "ANITA", "ANNA", "ANTONINA", "APOLONIA", "BALBINA", "BARBARA", "BEATA", "BERENIKA", "BERNADETA", "BERTA", "BLANKA", "BOGUMIŁA", "BOGUSŁAWA", "BOŻENA", "BRYGIDA", "CECYLIA", "CELINA", "CZESŁAWA", "DAGMARA", "DANIELA", "DANUTA", "DARIA", "DIANA", "DOMINIKA", "DOROTA", "EDYTA", "ELENA", "ELEONORA", "ELIZA", "ELWIRA", "ELŻBIETA", "EMILIA", "EUGENIA", "EWA", "EWELINA", "FELICJA", "FRANCISZKA", "GABRIELA", "GENOWEFA", "GERTRUDA", "GRAŻYNA", "HALINA", "HELENA", "HENRYKA", "HONORATA", "HUBERTA", "IDA", "IGA", "IRENA", "IRMINA", "IWONA", "IZA", "IZABELA", "JADWIGA", "JANINA", "JOANNA", "JOLANTA", "JUDYTA", "JULIA", "JUSTYNA", "KALINA", "KAMILA", "KAROLINA", "KATARZYNA", "KINGA", "KLARA", "KLAUDIA", "KLEMENTYNA", "KORNELIA", "KRYSTYNA", "LAURA", "LIDIA", "LILIANA", "LUCYNA", "LUDMIŁA", "LUDWIKA", "ŁUCJA", "MAGDA", "MAGDALENA", "MAJA", "MALWINA", "MAŁGORZATA", "MARCELINA", "MARIA", "MARLENA", "MARTA", "MARTYNA", "MARZENA", "MATYLDA", "MILENA", "MONIKA", "NATALIA", "NATASZA", "NINA", "OLGA", "OLIWIA", "OTYLIA", "PATRYCJA", "PAULINA", "REGINA", "RENATA", "ROKSANA", "ROZALIA", "RÓŻA", "SABINA", "SALOMEA", "SANDRA", "SŁAWOMIRA", "STEFANIA", "SYLWIA", "TEKLA", "TERESA", "URSZULA", "WACŁAWA", "WANDA", "WERONIKA", "WIKTORIA", "WIOLETTA", "WŁADYSŁAWA", "ZOFIA", "ZUZANNA", "ŻAKLINA", "ŻANETA", "ADAM", "ADRIAN", "ADOLF", "ALBERT", "ALBIN", "ALEKSANDER", "ALEKSY", "ALFONS", "ALFRED", "ALOJZY", "AMBROŻY", "ANATOL", "ANDRZEJ", "ANTONI", "ANZELM", "APOLINARY", "AURELIUSZ", "ARKADIUSZ", "ARTUR", "AUGUSTYN", "BARTOSZ", "BOGDAN", "BOGUSŁAW", "BOLESŁAW", "CEZARY", "CYPRIAN", "CZESŁAW", "DAMIAN", "DANIEL", "DARIUSZ", "DAWID", "DIONIZY", "DOMINIK", "EDWARD", "EMIL", "ERNEST", "ERYK", "FELIKS", "FILIP", "FRYDERYK", "GRZEGORZ", "GWIDON", "HENRYK", "HERBERT", "IRENEUSZ", "JACEK", "JAKUB", "JAN", "JAREK", "JERZY", "JULIUSZ", "KAJETAN", "KAMIL", "KACPER", "KAROL", "KAZIMIERZ", "KONRAD", "KRYSTIAN", "KRZYSZTOF", "LECH", "LEOPOLD", "LESZEK", "LUCJAN", "ŁUKASZ", "MACIEJ", "MAKSYMILIAN", "MARCIN", "MAREK", "MARIUSZ", "MATEUSZ", "MICHAŁ", "MIKOŁAJ", "MIROSŁAW", "NORBERT", "OLGIERD", "OSKAR", "PAWEŁ", "PATRYK", "PIOTR", "PRZEMYSŁAW", "RADOSŁAW", "RAFAŁ", "REMIGIUSZ", "ROBERT", "ROMAN", "SEBASTIAN", "SŁAWOMIR", "STANISŁAW", "STEFAN", "SYLWEK", "SZYMON", "TADEUSZ", "TOMASZ", "WACŁAW", "WALDEMAR", "WAWRZYNIEC", "WIKTOR", "WINCENTY", "WIT", "WITOLD", "WŁADYSŁAW", "WŁODZIMIERZ", "WOJCIECH", "ZBIGNIEW", "ZBYSZEK", "ZDZISŁAW", "ZENOBIUSZ", "ZENON", "ZYGMUNT", "ADA", "ADELA", "AGATA", "AGNIESZKA", "ALDONA", "ALEKSANDRA", "ALICJA", "ALINA", "ANASTAZJA", "ANETA", "ANGELIKA", "ANIELA", "ANITA", "ANNA", "ANTONINA", "APOLONIA", "BALBINA", "BARBARA", "BEATA", "BERENIKA", "BERNADETA", "BERTA", "BLANKA", "BOGUMIŁA", "BOGUSŁAWA", "BOŻENA", "BRYGIDA", "CECYLIA", "CELINA", "CZESŁAWA", "DAGMARA", "DANIELA", "DANUTA", "DARIA", "DIANA", "DOMINIKA", "DOROTA", "EDYTA", "ELENA", "ELEONORA", "ELIZA", "ELWIRA", "ELŻBIETA", "EMILIA", "EUGENIA", "EWA", "EWELINA", "FELICJA", "FRANCISZKA", "GABRIELA", "GENOWEFA", "GERTRUDA", "GRAŻYNA", "HALINA", "HELENA", "HENRYKA", "HONORATA", "HUBERTA", "IDA", "IGA", "IRENA", "IRMINA", "IWONA", "IZA", "IZABELA", "JADWIGA", "JANINA", "JOANNA", "JOLANTA", "JUDYTA", "JULIA", "JUSTYNA", "KALINA", "KAMILA", "KAROLINA", "KATARZYNA", "KINGA", "KLARA", "KLAUDIA", "KLEMENTYNA", "KORNELIA", "KRYSTYNA", "LAURA", "LIDIA", "LILIANA", "LUCYNA", "LUDMIŁA", "LUDWIKA", "ŁUCJA", "MAGDA", "MAGDALENA", "MAJA", "MALWINA", "MAŁGORZATA", "MARCELINA", "MARIA", "MARLENA", "MARTA", "MARTYNA", "MARZENA", "MATYLDA", "MILENA", "MONIKA", "NATALIA", "NATASZA", "NINA", "OLGA", "OLIWIA", "OTYLIA", "PATRYCJA", "PAULINA", "REGINA", "RENATA", "ROKSANA", "ROZALIA", "RÓŻA", "SABINA", "SALOMEA", "SANDRA", "SŁAWOMIRA", "STEFANIA", "SYLWIA", "TEKLA", "TERESA", "URSZULA", "WACŁAWA" };
		rounds.Add(new Round(RandomLetter()));
		
		if(Network.isServer)
		{
			GetComponent<NetworkView>().RPC("letterMessage", RPCMode.All, rounds[0].letter.ToString());
		}
		if(Network.isClient)
		{
			GameObject.Find("TestRESTART").GetComponent<Button>().interactable = false;
		}
		
		inputFields = new List<GameObject>();
		foreach (var go in GameObject.FindGameObjectsWithTag("CountryInputField"))
			inputFields.Add(go);
	}
	
	
	[RPC]
	public void letterMessage(string letter)
	{
		GameObject.Find("Letter").GetComponent<Text>().text = letter.ToString(); 
	}
	
	
	public void drukujOdpowiedzi()
	{
		Debug.LogError("Round count:" + rounds.Count);
		foreach (var round in rounds)
		{
			string print = "";
			Debug.LogError("LETTER: " + round.letter);
			foreach (var ans in round.playersAnswers)
			{
				print += ans.player.name + " { ";
				foreach (var an in ans.answers)
				{
					print += an.answer + "(" + an.points + ") ";
				}
				print += " }";
				Debug.Log(print);
				print = "";
			}
		}
	}
	
	public void buttonFinishRound()
	{
		GetComponent<NetworkView>().RPC("FinishRound", RPCMode.Others);
		disableCountryUI();
		EventSystem.current.SetSelectedGameObject(null, null);
		
		if (Network.isServer)
		{
			Debug.Log("getAnswers():" + getAnswers()[0] + " " + getAnswers()[1] + " " + getAnswers()[2] + " " + getAnswers()[3]);
			Answers tt = new Answers(new Player(GameObject.FindGameObjectWithTag("User").GetComponent<UserScript>().name, Network.player), getAnswers());
			rounds[rounds.Count - 1].playersAnswers.Add(tt);
		}
		else
		{
			string[] answers = getAnswers();
			GameObject.Find("GameCountry(Clone)").GetComponent<NetworkView>().RPC("sendAnswers", RPCMode.Server, GameObject.FindGameObjectWithTag("User").GetComponent<UserScript>().name, Network.player, answers[0], answers[1], answers[2], answers[3]);
		}
		
		
	}
	
	public void nextRound()
	{
		
		//char newLetter = (char)('A' + Random.Range(0, 26));
		char newLetter = RandomLetter(); //'A';// FOR TESTING!!
		//letterMessage("A");// FOR TESTING!!
		//letterMessage(newLetter.ToString());
		rounds.Add(new Round(newLetter));
		enableCountryUI();
		GetComponent<NetworkView>().RPC("letterMessage", RPCMode.All, newLetter.ToString()); // TESTING!!
		//networkView.RPC("letterMessage", RPCMode.All, newLetter.ToString());
		GetComponent<NetworkView>().RPC("enableCountryUI", RPCMode.All);
		
	}
	
	public string[] getAnswers()
	{
		string[] tempAnswers = new string[4];
		
		tempAnswers[0] = GameObject.FindGameObjectWithTag("CountryText").GetComponent<Text>().text;
		tempAnswers[1] = GameObject.FindGameObjectWithTag("TownText").GetComponent<Text>().text;
		tempAnswers[2] = GameObject.FindGameObjectWithTag("AnimalText").GetComponent<Text>().text;
		tempAnswers[3] = GameObject.FindGameObjectWithTag("NameText").GetComponent<Text>().text;
		return tempAnswers;
	}
	
	[RPC]
	public void enableCountryUI()
	{
		foreach (var i in GameObject.FindGameObjectsWithTag("CountryInputField"))
		{
			i.GetComponent<InputField>().text = "";
			i.GetComponent<InputField>().interactable = true;
		}
		GameObject.Find("EndRoundButton").GetComponent<Button>().interactable = true;
	}
	
	public void disableCountryUI()
	{
		foreach (var i in GameObject.FindGameObjectsWithTag("CountryInputField"))
		{
			i.GetComponent<InputField>().interactable = false;
		}
		GameObject.Find("EndRoundButton").GetComponent<Button>().interactable = false;
	}
	
	[RPC]
	public void sendAnswers(string playerName, NetworkPlayer networkPlayer, string country, string town, string animal, string name)
	{
		string[] tempAnswers = new string[4]{country,town,animal,name};
		Answers tt = new Answers(new Player(playerName,networkPlayer), tempAnswers);
		rounds[rounds.Count - 1].playersAnswers.Add(tt);
		
	}
	
	[RPC]
	void FinishRound()
	{
		GameObject.Instantiate(Resources.Load("Prefabs/Canvas Countdown"));
	}
	
	// Update is called once per frame
	void Update () 
	{
		//  Debug.Log("CZAS LECI" + Time.time);
	}
	
	
	public int countSameAnswer(string answerName, int answerCategory)
	{
		int count = 0;
		foreach (var round in rounds)
		{
			foreach (Answers ans in round.playersAnswers)
			{
				//        Debug.Log(ans.answers[answerCategory].ToString().ToUpper() + " == " + answerName.ToUpper());
				if (ans.answers[answerCategory].answer.ToString().ToUpper() == answerName.ToUpper())
				{
					count += 1;
					//            Debug.Log("ZGADZA SIE!: " + ans.answers[answerCategory].ToString().ToUpper() + " == " + answerName.ToUpper());
				}
			}
		}
		return count;
	}
	
	private int calculateScore(Player player)
	{
		int score = 0;
		foreach(var round in rounds)
		{
			foreach(var ans in round.playersAnswers)
			{
				if(ans.player.Equal(player))
					foreach(var answer in ans.answers)
				{
					score += answer.points;
				}
			}
		}
		Debug.LogError(score);
		return score;
	}
	private List<Player> getPlayers()
	{
		List<Player> tmp = new List<Player>();
		foreach(var ans in rounds[0].playersAnswers)
		{
			tmp.Add(ans.player);
		}
		return tmp;
	}
	
	public void distributeScore()
	{
		Debug.Log("ILE GRACZY: " +  getPlayers().Count);
		foreach(var player in getPlayers())
		{
			if (player.networkPlayer == Network.player || player.name == GameObject.FindGameObjectWithTag("User").GetComponent<UserScript>().name)
				setScore(calculateScore(player));
			else
				GetComponent<NetworkView>().RPC("setScore", player.networkPlayer, calculateScore(player));
		}
	}
	
	[RPC]
	public void setScore(int score)
	{
		this.score = score;
		GameObject.Find("Score").GetComponent<Text>().text = "Score: " + this.score.ToString();
	}
	
	[RPC] 
	public void RestartGame()
	{
		RestartGameImplementation();
	}
	public void RestartGameImplementation()
	{
		rounds = new List<Round>();
		rounds.Add(new Round(RandomLetter()));
		alphabet = "ABCDEFGHIJKLMNOPRSTUWZ";
		if (Network.isServer)
		{
			GetComponent<NetworkView>().RPC("letterMessage", RPCMode.All, rounds[0].letter.ToString());
		}
		setScore(0);
	}
	
	private bool checkAnswerInDatabase(string table, string answer)
	{
		Debug.Log("starting SQLiteLoad app");
		
		// Retrieve next word from database
		//description = "something went wrong with the database";
		
		dbAccess db = GetComponent<dbAccess>();
		
		db.OpenDB("db.db");
		
		ArrayList result = db.SingleSelectWhere("TOWN", "ANNOPOL");
		
		if (result.Count > 0)
		{
			db.CloseDB();
			return true;
			//description = "ISTNIEJE";
		}
		else
		{
			db.CloseDB();
			return false;
		}
		
	}
	
	public void CheckAnswer()
	{
		foreach (var round in rounds)
		{
			foreach (var ans in round.playersAnswers)
			{
				int i = 0;
				foreach (Answer an in ans.answers)
				{
					if(an.answer.Length >= 1)
						if (System.Char.ToUpper(an.answer[0]) == round.letter)
					{
						//               Debug.Log(an.answer + " " + an.answer.ToUpper() + " " + country.Exists(x => x == an.answer.ToUpper()) + " " + town.Contains(an.answer.ToUpper()) + " " + animal.Contains(an.answer.ToUpper()) + " " + name.Contains(an.answer.ToUpper()));
						switch(i)
						{
						case 0:
							if (checkAnswerInDatabase("COUNTRY",an.answer.ToUpper()))//country.Contains(an.answer.ToUpper()))
							{
								//     Debug.Log("ODPOWIEDZ: " + an.answer.ToUpper() + " wystapienia: " + countSameAnswer(an.answer.ToUpper(), 0));
								an.points = 20;
								if (countSameAnswer(an.answer.ToUpper(),0) == 2)
									an.points -= 5;
								else if (countSameAnswer(an.answer.ToUpper(), 0) >= 3)
									an.points -= 10;
							}
							break;
						case 1:
							if (checkAnswerInDatabase("TOWN", an.answer.ToUpper()))//town.Contains(an.answer.ToUpper()))
							{
								//        Debug.Log("ODPOWIEDZ: " + an.answer.ToUpper() + " wystapienia: " + countSameAnswer(an.answer.ToUpper(), 1));
								an.points = 20;
								if (countSameAnswer(an.answer.ToUpper(),1) == 2)
									an.points -= 5;
								else if (countSameAnswer(an.answer.ToUpper(), 1) >= 3)
									an.points -= 10;
							}
							break;
						case 2:
							if (checkAnswerInDatabase("ANIMAL",an.answer.ToUpper()))//animal.Contains(an.answer.ToUpper()))
							{
								//     Debug.Log("ODPOWIEDZ: " + an.answer.ToUpper() + " wystapienia: " + countSameAnswer(an.answer.ToUpper(), 2));
								an.points = 20;
								if (countSameAnswer(an.answer.ToUpper(),2) == 2)
									an.points -= 5;
								else if (countSameAnswer(an.answer.ToUpper(), 2) >= 3)
									an.points -= 10;
							}
							break;
						case 3:
							if (checkAnswerInDatabase("NAME",an.answer.ToUpper()))//name.Contains(an.answer.ToUpper()))
							{
								//    Debug.Log("ODPOWIEDZ: " + an.answer.ToUpper() + " wystapienia: " + countSameAnswer(an.answer.ToUpper(), 3));
								an.points = 20;
								if (countSameAnswer(an.answer.ToUpper(),3) == 2)
									an.points -= 5;
								else if (countSameAnswer(an.answer.ToUpper(), 3) >= 3)
									an.points -= 10;
							}
							break;
						}
					}
					i++;
				}
			}
		}
	}
	
}
