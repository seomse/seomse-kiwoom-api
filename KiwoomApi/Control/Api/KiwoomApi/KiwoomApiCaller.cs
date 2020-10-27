using KiwoomApi.Model.Def;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiwoomApi.Control.Api.KiwoomApi
{
    class KiwoomApiCaller
    {
        private static readonly string SUCCESS = "SUCCESS";
        private static readonly string FAIL = "FAIL";

        private static KiwoomApiController api = KiwoomApiController.Instance;


        ///<summary> 코드명:ORD10001 기능명:거래주문</summary>
        ///<param name="arg1">사용자구분 : 주식주문</param>
        ///<param name="arg3">계좌번호 : 계좌번호10자리</param>
        ///<param name="arg4">주문유형 : 1:신규매수, 2:신규매도 3:매수취소, 4:매도취소, 5:매수정정, 6:매도정정</param>
        ///<param name="arg5">종목코드 : 종목코드</param>
        ///<param name="arg6">주문수량 : 주문수량</param>
        ///<param name="arg7">주문가격 : 주문가격</param>
        ///<param name="arg8">호가구분 : 00:지정가,03:시장가,05:조건부지정가,06:최유리지정가,07:최우선지정가,10:지정가IOC,13:시장가IOC,16:최유리IOC,20:지정가FOK,23:시장가FOK,26:최유리FOK,61:장전시간외종가,62:시간외단일가매매,81:장후시간외종가</param>
        ///<param name="arg9">원주문번호 : 신규주문에는 공백, 정정(취소)주문할 원주문번호를 입력합니다.</param>
        public static string Order(string message)
        {
            Console.WriteLine("Order:" +  message);
            string[] paramArr = Separator.ParseData(message);
            string result = api.GetORD10001(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5], paramArr[6], paramArr[7]);
            if (result.Equals("0"))
            {
                return SUCCESS;
            } else
            {
                return FAIL;
            }
        }


        public static string CallTR(string trCode , string trMessage)
        {
            Console.WriteLine("CallTR:" + trCode + "," + trMessage);
            string[] paramArr = Separator.ParseData(trMessage);
            try
            {
                switch (trCode)
                {
                    //OPT10001 기능명:주식기본정보요청
                    case "OPT10001": if (paramArr.Length != 1) return FAIL; api.GetOPT10001(paramArr[0]); break;
                    //OPT10002 기능명:주식거래원요청
                    case "OPT10002": if (paramArr.Length != 1) return FAIL; api.GetOPT10002(paramArr[0]); break;
                    //OPT10003 기능명:체결정보요청
                    case "OPT10003": if (paramArr.Length != 1) return FAIL; api.GetOPT10003(paramArr[0]); break;
                    //OPT10004 기능명:주식호가요청
                    case "OPT10004": if (paramArr.Length != 1) return FAIL; api.GetOPT10004(paramArr[0]); break;
                    //OPT10005 기능명:주식일주월시분요청
                    case "OPT10005": if (paramArr.Length != 1) return FAIL; api.GetOPT10005(paramArr[0]); break;
                    //OPT10006 기능명:주식시분요청
                    case "OPT10006": if (paramArr.Length != 1) return FAIL; api.GetOPT10006(paramArr[0]); break;
                    //OPT10007 기능명:시세표성정보요청
                    case "OPT10007": if (paramArr.Length != 1) return FAIL; api.GetOPT10007(paramArr[0]); break;
                    //OPT10008 기능명:주식외국인요청
                    case "OPT10008": if (paramArr.Length != 2) return FAIL; api.GetOPT10008(paramArr[0], paramArr[1]); break;
                    //OPT10009 기능명:주식기관요청
                    case "OPT10009": if (paramArr.Length != 1) return FAIL; api.GetOPT10009(paramArr[0]); break;
                    //OPT10010 기능명:업종프로그램요청
                    case "OPT10010": if (paramArr.Length != 1) return FAIL; api.GetOPT10010(paramArr[0]); break;
                    //OPT10012 기능명:주문체결요청
                    case "OPT10012": if (paramArr.Length != 1) return FAIL; api.GetOPT10012(paramArr[0]); break;
                    //OPT10013 기능명:신용매매동향요청
                    case "OPT10013": if (paramArr.Length != 3) return FAIL; api.GetOPT10013(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT10014 기능명:공매도추이요청
                    case "OPT10014": if (paramArr.Length != 4) return FAIL; api.GetOPT10014(paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT10015 기능명:일별거래상세요청
                    case "OPT10015": if (paramArr.Length != 2) return FAIL; api.GetOPT10015(paramArr[0], paramArr[1]); break;
                    //OPT10016 기능명:신고저가요청
                    case "OPT10016": if (paramArr.Length != 8) return FAIL; api.GetOPT10016(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5], paramArr[6], paramArr[7]); break;
                    //OPT10017 기능명:상하한가요청
                    case "OPT10017": if (paramArr.Length != 7) return FAIL; api.GetOPT10017(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5], paramArr[6]); break;
                    //OPT10018 기능명:고저가근접요청
                    case "OPT10018": if (paramArr.Length != 6) return FAIL; api.GetOPT10018(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5]); break;
                    //OPT10019 기능명:가격급등락요청
                    case "OPT10019": if (paramArr.Length != 9) return FAIL; api.GetOPT10019(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5], paramArr[6], paramArr[7], paramArr[8]); break;
                    //OPT10020 기능명:호가잔량상위요청
                    case "OPT10020": if (paramArr.Length != 5) return FAIL; api.GetOPT10020(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4]); break;
                    //OPT10021 기능명:호가잔량급증요청
                    case "OPT10021": if (paramArr.Length != 6) return FAIL; api.GetOPT10021(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5]); break;
                    //OPT10022 기능명:잔량율급증요청
                    case "OPT10022": if (paramArr.Length != 5) return FAIL; api.GetOPT10022(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4]); break;
                    //OPT10023 기능명:거래량급증요청
                    case "OPT10023": if (paramArr.Length != 7) return FAIL; api.GetOPT10023(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5], paramArr[6]); break;
                    //OPT10024 기능명:거래량갱신요청
                    case "OPT10024": if (paramArr.Length != 3) return FAIL; api.GetOPT10024(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT10025 기능명:매물대집중요청
                    case "OPT10025": if (paramArr.Length != 5) return FAIL; api.GetOPT10025(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4]); break;
                    //OPT10026 기능명:고저PER요청
                    case "OPT10026": if (paramArr.Length != 1) return FAIL; api.GetOPT10026(paramArr[0]); break;
                    //OPT10027 기능명:전일대비등락률상위요청
                    case "OPT10027": if (paramArr.Length != 8) return FAIL; api.GetOPT10027(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5], paramArr[6], paramArr[7]); break;
                    //OPT10028 기능명:시가대비등락률요청
                    case "OPT10028": if (paramArr.Length != 8) return FAIL; api.GetOPT10028(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5], paramArr[6], paramArr[7]); break;
                    //OPT10029 기능명:예상체결등락률상위요청
                    case "OPT10029": if (paramArr.Length != 6) return FAIL; api.GetOPT10029(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5]); break;
                    //OPT10030 기능명:당일거래량상위요청
                    case "OPT10030": if (paramArr.Length != 3) return FAIL; api.GetOPT10030(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT10031 기능명:전일거래량상위요청
                    case "OPT10031": if (paramArr.Length != 4) return FAIL; api.GetOPT10031(paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT10032 기능명:거래대금상위요청
                    case "OPT10032": if (paramArr.Length != 2) return FAIL; api.GetOPT10032(paramArr[0], paramArr[1]); break;
                    //OPT10033 기능명:신용비율상위요청
                    case "OPT10033": if (paramArr.Length != 5) return FAIL; api.GetOPT10033(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4]); break;
                    //OPT10034 기능명:외인기간별매매상위요청
                    case "OPT10034": if (paramArr.Length != 3) return FAIL; api.GetOPT10034(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT10035 기능명:외인연속순매매상위요청
                    case "OPT10035": if (paramArr.Length != 3) return FAIL; api.GetOPT10035(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT10036 기능명:매매상위요청
                    case "OPT10036": if (paramArr.Length != 2) return FAIL; api.GetOPT10036(paramArr[0], paramArr[1]); break;
                    //OPT10037 기능명:외국계창구매매상위요청
                    case "OPT10037": if (paramArr.Length != 6) return FAIL; api.GetOPT10037(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5]); break;
                    //OPT10038 기능명:종목별증권사순위요청
                    case "OPT10038": if (paramArr.Length != 5) return FAIL; api.GetOPT10038(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4]); break;
                    //OPT10039 기능명:증권사별매매상위요청
                    case "OPT10039": if (paramArr.Length != 4) return FAIL; api.GetOPT10039(paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT10040 기능명:당일주요거래원요청
                    case "OPT10040": if (paramArr.Length != 1) return FAIL; api.GetOPT10040(paramArr[0]); break;
                    //OPT10041 기능명:조기종료통화단위요청
                    case "OPT10041": if (paramArr.Length != 2) return FAIL; api.GetOPT10041(paramArr[0], paramArr[1]); break;
                    //OPT10042 기능명:순매수거래원순위요청
                    case "OPT10042": if (paramArr.Length != 7) return FAIL; api.GetOPT10042(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5], paramArr[6]); break;
                    //OPT10043 기능명:거래원매물대분석요청
                    case "OPT10043": if (paramArr.Length != 8) return FAIL; api.GetOPT10043(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5], paramArr[6], paramArr[7]); break;
                    //OPT10044 기능명:일별기관매매종목요청
                    case "OPT10044": if (paramArr.Length != 4) return FAIL; api.GetOPT10044(paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT10045 기능명:종목별기관매매추이요청
                    case "OPT10045": if (paramArr.Length != 7) return FAIL; api.GetOPT10045(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5], paramArr[6]); break;
                    //OPT10048 기능명:ELW일별민감도지표요청
                    case "OPT10048": if (paramArr.Length != 1) return FAIL; api.GetOPT10048(paramArr[0]); break;
                    //OPT10049 기능명:ELW투자지표요청
                    case "OPT10049": if (paramArr.Length != 3) return FAIL; api.GetOPT10049(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT10050 기능명:ELW민감도지표요청
                    case "OPT10050": if (paramArr.Length != 1) return FAIL; api.GetOPT10050(paramArr[0]); break;
                    //OPT10051 기능명:업종별투자자순매수요청
                    case "OPT10051": if (paramArr.Length != 3) return FAIL; api.GetOPT10051(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT10052 기능명:거래원순간거래량요청
                    case "OPT10052": if (paramArr.Length != 4) return FAIL; api.GetOPT10052(paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT10053 기능명:당일상위이탈원요청
                    case "OPT10053": if (paramArr.Length != 1) return FAIL; api.GetOPT10053(paramArr[0]); break;
                    //OPT10054 기능명:변동성완화장치발동종목요청
                    case "OPT10054": if (paramArr.Length != 11) return FAIL; api.GetOPT10054(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5], paramArr[6], paramArr[7], paramArr[8], paramArr[9], paramArr[10]); break;
                    //OPT10055 기능명:당일전일체결대량요청
                    case "OPT10055": if (paramArr.Length != 2) return FAIL; api.GetOPT10055(paramArr[0], paramArr[1]); break;
                    //OPT10058 기능명:투자자별일별매매종목요청
                    case "OPT10058": if (paramArr.Length != 5) return FAIL; api.GetOPT10058(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4]); break;
                    //OPT10059 기능명:종목별투자자기관별요청
                    case "OPT10059": if (paramArr.Length != 5) return FAIL; api.GetOPT10059(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4]); break;
                    //OPT10060 기능명:종목별투자자기관별차트요청
                    case "OPT10060": if (paramArr.Length != 5) return FAIL; api.GetOPT10060(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4]); break;
                    //OPT10061 기능명:종목별투자자기관별합계요청
                    case "OPT10061": if (paramArr.Length != 6) return FAIL; api.GetOPT10061(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5]); break;
                    //OPT10062 기능명:동일순매매순위요청
                    case "OPT10062": if (paramArr.Length != 6) return FAIL; api.GetOPT10062(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5]); break;
                    //OPT10063 기능명:장중투자자별매매요청
                    case "OPT10063": if (paramArr.Length != 5) return FAIL; api.GetOPT10063(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4]); break;
                    //OPT10064 기능명:장중투자자별매매차트요청
                    case "OPT10064": if (paramArr.Length != 4) return FAIL; api.GetOPT10064(paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT10065 기능명:장중투자자별매매상위요청
                    case "OPT10065": if (paramArr.Length != 3) return FAIL; api.GetOPT10065(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT10066 기능명:장중투자자별매매차트요청
                    case "OPT10066": if (paramArr.Length != 4) return FAIL; api.GetOPT10066(paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT10067 기능명:대차거래내역요청
                    case "OPT10067": if (paramArr.Length != 2) return FAIL; api.GetOPT10067(paramArr[0], paramArr[1]); break;
                    //OPT10068 기능명:대차거래추이요청
                    case "OPT10068": if (paramArr.Length != 4) return FAIL; api.GetOPT10068(paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT10069 기능명:대차거래상위10종목요청
                    case "OPT10069": if (paramArr.Length != 3) return FAIL; api.GetOPT10069(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT10070 기능명:당일주요거래원요청
                    case "OPT10070": if (paramArr.Length != 1) return FAIL; api.GetOPT10070(paramArr[0]); break;
                    //OPT10071 기능명:시간대별전일비거래비중요청
                    case "OPT10071": if (paramArr.Length != 2) return FAIL; api.GetOPT10071(paramArr[0], paramArr[1]); break;
                    //OPT10072 기능명:일자별종목별실현손익요청
                    case "OPT10072": if (paramArr.Length != 3) return FAIL; api.GetOPT10072(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT10073 기능명:일자별종목별실현손익요청
                    case "OPT10073": if (paramArr.Length != 4) return FAIL; api.GetOPT10073(paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT10074 기능명:일자별실현손익요청
                    case "OPT10074": if (paramArr.Length != 3) return FAIL; api.GetOPT10074(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT10075 기능명:실시간미체결요청
                    case "OPT10075": if (paramArr.Length != 3) return FAIL; api.GetOPT10075(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT10076 기능명:실시간체결요청
                    case "OPT10076": if (paramArr.Length != 6) return FAIL; api.GetOPT10076(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5]); break;
                    //OPT10077 기능명:당일실현손익상세요청
                    case "OPT10077": if (paramArr.Length != 3) return FAIL; api.GetOPT10077(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT10078 기능명:증권사별종목매매동향요청
                    case "OPT10078": if (paramArr.Length != 4) return FAIL; api.GetOPT10078(paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT10079 기능명:주식틱차트조회요청
                    case "OPT10079": if (paramArr.Length != 3) return FAIL; api.GetOPT10079(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT10080 기능명:주식분봉차트조회요청
                    case "OPT10080": if (paramArr.Length != 3) return FAIL; api.GetOPT10080(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT10081 기능명:주식일봉차트조회요청
                    case "OPT10081": if (paramArr.Length != 3) return FAIL; api.GetOPT10081(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT10082 기능명:주식주봉차트조회요청
                    case "OPT10082": if (paramArr.Length != 3) return FAIL; api.GetOPT10082(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT10083 기능명:주식월봉차트조회요청
                    case "OPT10083": if (paramArr.Length != 3) return FAIL; api.GetOPT10083(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT10084 기능명:당일전일체결요청
                    case "OPT10084": if (paramArr.Length != 4) return FAIL; api.GetOPT10084(paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT10085 기능명:계좌수익률요청
                    case "OPT10085": if (paramArr.Length != 1) return FAIL; api.GetOPT10085(paramArr[0]); break;
                    //OPT10086 기능명:일별주가요청
                    case "OPT10086": if (paramArr.Length != 3) return FAIL; api.GetOPT10086(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT10087 기능명:시간외단일가요청
                    case "OPT10087": if (paramArr.Length != 1) return FAIL; api.GetOPT10087(paramArr[0]); break;
                    //OPT10094 기능명:주식년봉차트조회요청
                    case "OPT10094": if (paramArr.Length != 4) return FAIL; api.GetOPT10094(paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT20001 기능명:업종현재가요청
                    case "OPT20001": if (paramArr.Length != 2) return FAIL; api.GetOPT20001(paramArr[0], paramArr[1]); break;
                    //OPT20002 기능명:업종별주가요청
                    case "OPT20002": if (paramArr.Length != 2) return FAIL; api.GetOPT20002(paramArr[0], paramArr[1]); break;
                    //OPT20003 기능명:전업종지수요청
                    case "OPT20003": if (paramArr.Length != 1) return FAIL; api.GetOPT20003(paramArr[0]); break;
                    //OPT20004 기능명:업종틱차트조회요청
                    case "OPT20004": if (paramArr.Length != 3) return FAIL; api.GetOPT20004(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT20005 기능명:업종분봉조회요청
                    case "OPT20005": if (paramArr.Length != 3) return FAIL; api.GetOPT20005(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT20006 기능명:업종일봉조회요청
                    case "OPT20006": if (paramArr.Length != 3) return FAIL; api.GetOPT20006(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT20007 기능명:업종주봉조회요청
                    case "OPT20007": if (paramArr.Length != 3) return FAIL; api.GetOPT20007(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT20008 기능명:업종월봉조회요청
                    case "OPT20008": if (paramArr.Length != 3) return FAIL; api.GetOPT20008(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT20009 기능명:업종현재가일별요청
                    case "OPT20009": if (paramArr.Length != 2) return FAIL; api.GetOPT20009(paramArr[0], paramArr[1]); break;
                    //OPT20019 기능명:업종년봉조회요청
                    case "OPT20019": if (paramArr.Length != 3) return FAIL; api.GetOPT20019(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT20068 기능명:대차거래추이요청(종목별)
                    case "OPT20068": if (paramArr.Length != 4) return FAIL; api.GetOPT20068(paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT30001 기능명:ELW가격급등락요청
                    case "OPT30001": if (paramArr.Length != 9) return FAIL; api.GetOPT30001(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5], paramArr[6], paramArr[7], paramArr[8]); break;
                    //OPT30002 기능명:거래원별ELW순매매상위요청
                    case "OPT30002": if (paramArr.Length != 5) return FAIL; api.GetOPT30002(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4]); break;
                    //OPT30003 기능명:ELW LP보유일별추이요청
                    case "OPT30003": if (paramArr.Length != 2) return FAIL; api.GetOPT30003(paramArr[0], paramArr[1]); break;
                    //OPT30004 기능명:ELW괴리율요청
                    case "OPT30004": if (paramArr.Length != 5) return FAIL; api.GetOPT30004(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4]); break;
                    //OPT30005 기능명:ELW조건검색요청
                    case "OPT30005": if (paramArr.Length != 5) return FAIL; api.GetOPT30005(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4]); break;
                    //OPT30007 기능명:ELW종목상세요청
                    case "OPT30007": if (paramArr.Length != 6) return FAIL; api.GetOPT30007(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5]); break;
                    //OPT30008 기능명:ELW민감도지표요청
                    case "OPT30008": if (paramArr.Length != 1) return FAIL; api.GetOPT30008(paramArr[0]); break;
                    //OPT30009 기능명:ELW등락율순위요청
                    case "OPT30009": if (paramArr.Length != 3) return FAIL; api.GetOPT30009(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT30010 기능명:ELW잔량순위요청
                    case "OPT30010": if (paramArr.Length != 3) return FAIL; api.GetOPT30010(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT30011 기능명:ELW근접율요청
                    case "OPT30011": if (paramArr.Length != 1) return FAIL; api.GetOPT30011(paramArr[0]); break;
                    //OPT30012 기능명:ELW종목상세정보요청
                    case "OPT30012": if (paramArr.Length != 1) return FAIL; api.GetOPT30012(paramArr[0]); break;
                    //OPT40001 기능명:ETF수익율요청
                    case "OPT40001": if (paramArr.Length != 3) return FAIL; api.GetOPT40001(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT40002 기능명:ETF종목정보요청
                    case "OPT40002": if (paramArr.Length != 1) return FAIL; api.GetOPT40002(paramArr[0]); break;
                    //OPT40003 기능명:ETF일별추이요청
                    case "OPT40003": if (paramArr.Length != 1) return FAIL; api.GetOPT40003(paramArr[0]); break;
                    //OPT40004 기능명:ETF전체시세요청
                    case "OPT40004": if (paramArr.Length != 5) return FAIL; api.GetOPT40004(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4]); break;
                    //OPT40005 기능명:ETF일별추이요청
                    case "OPT40005": if (paramArr.Length != 1) return FAIL; api.GetOPT40005(paramArr[0]); break;
                    //OPT40006 기능명:ETF시간대별추이요청
                    case "OPT40006": if (paramArr.Length != 5) return FAIL; api.GetOPT40006(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4]); break;
                    //OPT40007 기능명:ETF시간대별체결요청
                    case "OPT40007": if (paramArr.Length != 1) return FAIL; api.GetOPT40007(paramArr[0]); break;
                    //OPT40008 기능명:ETF시간대별체결요청
                    case "OPT40008": if (paramArr.Length != 1) return FAIL; api.GetOPT40008(paramArr[0]); break;
                    //OPT40009 기능명:ETF시간대별체결요청
                    case "OPT40009": if (paramArr.Length != 1) return FAIL; api.GetOPT40009(paramArr[0]); break;
                    //OPT40010 기능명:ETF시간대별추이요청
                    case "OPT40010": if (paramArr.Length != 1) return FAIL; api.GetOPT40010(paramArr[0]); break;
                    //OPT50001 기능명:선옵현재가정보요청
                    case "OPT50001": if (paramArr.Length != 1) return FAIL; api.GetOPT50001(paramArr[0]); break;
                    //OPT50002 기능명:선옵일자별체결요청
                    case "OPT50002": if (paramArr.Length != 1) return FAIL; api.GetOPT50002(paramArr[0]); break;
                    //OPT50003 기능명:선옵시고저가요청
                    case "OPT50003": if (paramArr.Length != 1) return FAIL; api.GetOPT50003(paramArr[0]); break;
                    //OPT50004 기능명:콜옵션행사가요청
                    case "OPT50004": if (paramArr.Length != 1) return FAIL; api.GetOPT50004(paramArr[0]); break;
                    //OPT50005 기능명:선옵시간별거래량요청
                    case "OPT50005": if (paramArr.Length != 1) return FAIL; api.GetOPT50005(paramArr[0]); break;
                    //OPT50006 기능명:선옵체결추이요청
                    case "OPT50006": if (paramArr.Length != 1) return FAIL; api.GetOPT50006(paramArr[0]); break;
                    //OPT50007 기능명:선물시세추이요청
                    case "OPT50007": if (paramArr.Length != 3) return FAIL; api.GetOPT50007(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT50008 기능명:프로그램매매추이차트요청
                    case "OPT50008": if (paramArr.Length != 2) return FAIL; api.GetOPT50008(paramArr[0], paramArr[1]); break;
                    //OPT50009 기능명:선옵시간별잔량요청
                    case "OPT50009": if (paramArr.Length != 2) return FAIL; api.GetOPT50009(paramArr[0], paramArr[1]); break;
                    //OPT50010 기능명:선옵호가잔량추이요청
                    case "OPT50010": if (paramArr.Length != 2) return FAIL; api.GetOPT50010(paramArr[0], paramArr[1]); break;
                    //OPT50011 기능명:선옵호가잔량추이요청
                    case "OPT50011": if (paramArr.Length != 3) return FAIL; api.GetOPT50011(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT50012 기능명:선옵타임스프레드차트요청
                    case "OPT50012": if (paramArr.Length != 2) return FAIL; api.GetOPT50012(paramArr[0], paramArr[1]); break;
                    //OPT50013 기능명:선물가격대별비중차트요청
                    case "OPT50013": if (paramArr.Length != 2) return FAIL; api.GetOPT50013(paramArr[0], paramArr[1]); break;
                    //OPT50014 기능명:선물가격대별비중차트요청
                    case "OPT50014": if (paramArr.Length != 2) return FAIL; api.GetOPT50014(paramArr[0], paramArr[1]); break;
                    //OPT50015 기능명:선물미결제약정일차트요청
                    case "OPT50015": if (paramArr.Length != 2) return FAIL; api.GetOPT50015(paramArr[0], paramArr[1]); break;
                    //OPT50016 기능명:베이시스추이차트요청
                    case "OPT50016": if (paramArr.Length != 2) return FAIL; api.GetOPT50016(paramArr[0], paramArr[1]); break;
                    //OPT50017 기능명:베이시스추이차트요청
                    case "OPT50017": if (paramArr.Length != 2) return FAIL; api.GetOPT50017(paramArr[0], paramArr[1]); break;
                    //OPT50018 기능명:풋콜옵션비율차트요청
                    case "OPT50018": if (paramArr.Length != 1) return FAIL; api.GetOPT50018(paramArr[0]); break;
                    //OPT50019 기능명:선물옵션현재가정보요청
                    case "OPT50019": if (paramArr.Length != 1) return FAIL; api.GetOPT50019(paramArr[0]); break;
                    //OPT50020 기능명:복수종목결제월별시세요청
                    case "OPT50020": if (paramArr.Length != 1) return FAIL; api.GetOPT50020(paramArr[0]); break;
                    //OPT50021 기능명:콜종목결제월별시세요청
                    case "OPT50021": if (paramArr.Length != 1) return FAIL; api.GetOPT50021(paramArr[0]); break;
                    //OPT50022 기능명:풋종목결제월별시세요청
                    case "OPT50022": if (paramArr.Length != 1) return FAIL; api.GetOPT50022(paramArr[0]); break;
                    //OPT50023 기능명:민감도지표추이요청
                    case "OPT50023": if (paramArr.Length != 2) return FAIL; api.GetOPT50023(paramArr[0], paramArr[1]); break;
                    //OPT50024 기능명:일별변동성분석그래프요청
                    case "OPT50024": if (paramArr.Length != 4) return FAIL; api.GetOPT50024(paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT50025 기능명:시간별변동성분석그래프요청
                    case "OPT50025": if (paramArr.Length != 4) return FAIL; api.GetOPT50025(paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT50026 기능명:선옵주문체결요청
                    case "OPT50026": if (paramArr.Length != 4) return FAIL; api.GetOPT50026(paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT50027 기능명:선옵잔고요청
                    case "OPT50027": if (paramArr.Length != 1) return FAIL; api.GetOPT50027(paramArr[0]); break;
                    //OPT50028 기능명:선물틱차트요청
                    case "OPT50028": if (paramArr.Length != 2) return FAIL; api.GetOPT50028(paramArr[0], paramArr[1]); break;
                    //OPT50029 기능명:선물분차트요청
                    case "OPT50029": if (paramArr.Length != 2) return FAIL; api.GetOPT50029(paramArr[0], paramArr[1]); break;
                    //OPT50030 기능명:선물일차트요청
                    case "OPT50030": if (paramArr.Length != 2) return FAIL; api.GetOPT50030(paramArr[0], paramArr[1]); break;
                    //OPT50031 기능명:선옵잔고손익요청
                    case "OPT50031": if (paramArr.Length != 1) return FAIL; api.GetOPT50031(paramArr[0]); break;
                    //OPT50032 기능명:선옵당일실현손익요청
                    case "OPT50032": if (paramArr.Length != 2) return FAIL; api.GetOPT50032(paramArr[0], paramArr[1]); break;
                    //OPT50033 기능명:선옵잔존일조회요청
                    case "OPT50033": if (paramArr.Length != 2) return FAIL; api.GetOPT50033(paramArr[0], paramArr[1]); break;
                    //OPT50034 기능명:선옵전일가격요청
                    case "OPT50034": if (paramArr.Length != 2) return FAIL; api.GetOPT50034(paramArr[0], paramArr[1]); break;
                    //OPT50035 기능명:지수변동성차트요청
                    case "OPT50035": if (paramArr.Length != 4) return FAIL; api.GetOPT50035(paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT50036 기능명:주요지수변동성차트요청
                    case "OPT50036": if (paramArr.Length != 4) return FAIL; api.GetOPT50036(paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT50037 기능명:코스피200지수요청
                    case "OPT50037": if (paramArr.Length != 2) return FAIL; api.GetOPT50037(paramArr[0], paramArr[1]); break;
                    //OPT50038 기능명:투자자별만기손익차트요청
                    case "OPT50038": if (paramArr.Length != 4) return FAIL; api.GetOPT50038(paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT50040 기능명:선옵시고저가요청
                    case "OPT50040": if (paramArr.Length != 1) return FAIL; api.GetOPT50040(paramArr[0]); break;
                    //OPT50043 기능명:주식선물거래량상위종목요청
                    case "OPT50043": if (paramArr.Length != 1) return FAIL; api.GetOPT50043(paramArr[0]); break;
                    //OPT50044 기능명:주식선물시세표요청
                    case "OPT50044": if (paramArr.Length != 1) return FAIL; api.GetOPT50044(paramArr[0]); break;
                    //OPT50062 기능명:선물미결제약정분차트요청
                    case "OPT50062": if (paramArr.Length != 2) return FAIL; api.GetOPT50062(paramArr[0], paramArr[1]); break;
                    //OPT50063 기능명:옵션미결제약정일차트요청
                    case "OPT50063": if (paramArr.Length != 2) return FAIL; api.GetOPT50063(paramArr[0], paramArr[1]); break;
                    //OPT50064 기능명:옵션미결제약정분차트요청
                    case "OPT50064": if (paramArr.Length != 2) return FAIL; api.GetOPT50064(paramArr[0], paramArr[1]); break;
                    //OPT50065 기능명:풋옵션행사가요청
                    case "OPT50065": if (paramArr.Length != 1) return FAIL; api.GetOPT50065(paramArr[0]); break;
                    //OPT50066 기능명:옵션틱차트요청
                    case "OPT50066": if (paramArr.Length != 2) return FAIL; api.GetOPT50066(paramArr[0], paramArr[1]); break;
                    //OPT50067 기능명:옵션분차트요청
                    case "OPT50067": if (paramArr.Length != 2) return FAIL; api.GetOPT50067(paramArr[0], paramArr[1]); break;
                    //OPT50068 기능명:옵션일차트요청
                    case "OPT50068": if (paramArr.Length != 2) return FAIL; api.GetOPT50068(paramArr[0], paramArr[1]); break;
                    //OPT50071 기능명:선물주차트요청
                    case "OPT50071": if (paramArr.Length != 2) return FAIL; api.GetOPT50071(paramArr[0], paramArr[1]); break;
                    //OPT50072 기능명:선물월차트요청
                    case "OPT50072": if (paramArr.Length != 2) return FAIL; api.GetOPT50072(paramArr[0], paramArr[1]); break;
                    //OPT50073 기능명:선물년차트요청
                    case "OPT50073": if (paramArr.Length != 2) return FAIL; api.GetOPT50073(paramArr[0], paramArr[1]); break;
                    //OPT90001 기능명:테마그룹별요청
                    case "OPT90001": if (paramArr.Length != 5) return FAIL; api.GetOPT90001(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4]); break;
                    //OPT90002 기능명:테마구성종목요청
                    case "OPT90002": if (paramArr.Length != 2) return FAIL; api.GetOPT90002(paramArr[0], paramArr[1]); break;
                    //OPT90003 기능명:프로그램순매수상위50요청
                    case "OPT90003": if (paramArr.Length != 3) return FAIL; api.GetOPT90003(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT90004 기능명:종목별프로그램매매현황요청
                    case "OPT90004": if (paramArr.Length != 2) return FAIL; api.GetOPT90004(paramArr[0], paramArr[1]); break;
                    //OPT90005 기능명:프로그램매매추이요청
                    case "OPT90005": if (paramArr.Length != 5) return FAIL; api.GetOPT90005(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4]); break;
                    //OPT90006 기능명:프로그램매매차익잔고추이요청
                    case "OPT90006": if (paramArr.Length != 1) return FAIL; api.GetOPT90006(paramArr[0]); break;
                    //OPT90007 기능명:프로그램매매누적추이요청
                    case "OPT90007": if (paramArr.Length != 3) return FAIL; api.GetOPT90007(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT90008 기능명:종목시간별프로그램매매추이요청
                    case "OPT90008": if (paramArr.Length != 4) return FAIL; api.GetOPT90008(paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT90009 기능명:외국인기관매매상위요청
                    case "OPT90009": if (paramArr.Length != 4) return FAIL; api.GetOPT90009(paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT90010 기능명:차익잔고현황요청
                    case "OPT90010": if (paramArr.Length != 2) return FAIL; api.GetOPT90010(paramArr[0], paramArr[1]); break;
                    //OPT90011 기능명:차익잔고현황요청
                    case "OPT90011": if (paramArr.Length != 2) return FAIL; api.GetOPT90011(paramArr[0], paramArr[1]); break;
                    //OPT90012 기능명:대차거래내역요청
                    case "OPT90012": if (paramArr.Length != 2) return FAIL; api.GetOPT90012(paramArr[0], paramArr[1]); break;
                    //OPT90013 기능명:종목일별프로그램매매추이요청
                    case "OPT90013": if (paramArr.Length != 4) return FAIL; api.GetOPT90013(paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT99999 기능명:대차거래상위10종목요청
                    case "OPT99999": if (paramArr.Length != 3) return FAIL; api.GetOPT99999(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPTFOFID 기능명:선물전체시세요청
                    case "OPTFOFID": if (paramArr.Length != 1) return FAIL; api.GetOPTFOFID(paramArr[0]); break;
                    //OPTKWFID 기능명:관심종목정보요청
                    case "OPTKWFID": if (paramArr.Length != 1) return FAIL; api.GetOPTKWFID(paramArr[0]); break;
                    //OPTKWINV 기능명:관심종목투자자정보요청
                    case "OPTKWINV": if (paramArr.Length != 1) return FAIL; api.GetOPTKWINV(paramArr[0]); break;
                    //OPTKWPRO 기능명:관심종목프로그램정보요청
                    case "OPTKWPRO": if (paramArr.Length != 1) return FAIL; api.GetOPTKWPRO(paramArr[0]); break;
                    //OPW00001 기능명:예수금상세현황요청
                    case "OPW00001": if (paramArr.Length != 4) return FAIL; api.GetOPW00001(paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPW00002 기능명:일별추정예탁자산현황요청
                    case "OPW00002": if (paramArr.Length != 4) return FAIL; api.GetOPW00002(paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPW00003 기능명:추정자산조회요청
                    case "OPW00003": if (paramArr.Length != 3) return FAIL; api.GetOPW00003(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPW00004 기능명:계좌평가현황요청
                    case "OPW00004": if (paramArr.Length != 4) return FAIL; api.GetOPW00004(paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPW00005 기능명:체결잔고요청
                    case "OPW00005": if (paramArr.Length != 3) return FAIL; api.GetOPW00005(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPW00006 기능명:관리자별주문체결내역요청
                    case "OPW00006": if (paramArr.Length != 3) return FAIL; api.GetOPW00006(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPW00007 기능명:계좌별주문체결내역상세요청
                    case "OPW00007": if (paramArr.Length != 9) return FAIL; api.GetOPW00007(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5], paramArr[6], paramArr[7], paramArr[8]); break;
                    //OPW00008 기능명:계좌별익일결제예정내역요청
                    case "OPW00008": if (paramArr.Length != 4) return FAIL; api.GetOPW00008(paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPW00009 기능명:계좌별주문체결현황요청
                    case "OPW00009": if (paramArr.Length != 10) return FAIL; api.GetOPW00009(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5], paramArr[6], paramArr[7], paramArr[8], paramArr[9]); break;
                    //OPW00010 기능명:주문인출가능금액요청
                    case "OPW00010": if (paramArr.Length != 9) return FAIL; api.GetOPW00010(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5], paramArr[6], paramArr[7], paramArr[8]); break;
                    //OPW00011 기능명:증거금율별주문가능수량조회요청
                    case "OPW00011": if (paramArr.Length != 5) return FAIL; api.GetOPW00011(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4]); break;
                    //OPW00012 기능명:신용보증금율별주문가능수량조회요청
                    case "OPW00012": if (paramArr.Length != 5) return FAIL; api.GetOPW00012(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4]); break;
                    //OPW00013 기능명:증거금세부내역조회요청
                    case "OPW00013": if (paramArr.Length != 2) return FAIL; api.GetOPW00013(paramArr[0], paramArr[1]); break;
                    //OPW00014 기능명:비밀번호일치여부요청
                    case "OPW00014": if (paramArr.Length != 3) return FAIL; api.GetOPW00014(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPW00015 기능명:위탁종합거래내역요청
                    case "OPW00015": if (paramArr.Length != 10) return FAIL; api.GetOPW00015(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5], paramArr[6], paramArr[7], paramArr[8], paramArr[9]); break;
                    //OPW00016 기능명:일별계좌수익률상세현황요청
                    case "OPW00016": if (paramArr.Length != 5) return FAIL; api.GetOPW00016(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4]); break;
                    //OPW00017 기능명:계좌별당일현황요청
                    case "OPW00017": if (paramArr.Length != 3) return FAIL; api.GetOPW00017(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPW00018 기능명:계좌평가잔고내역요청
                    case "OPW00018": if (paramArr.Length != 4) return FAIL; api.GetOPW00018(paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPW10001 기능명:ELW종목별민감도지표요청
                    case "OPW10001": if (paramArr.Length != 3) return FAIL; api.GetOPW10001(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPW10002 기능명:ELW투자지표요청
                    case "OPW10002": if (paramArr.Length != 3) return FAIL; api.GetOPW10002(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPW10003 기능명:ELW민감도지표요청
                    case "OPW10003": if (paramArr.Length != 3) return FAIL; api.GetOPW10003(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPW10004 기능명:업종별순매수요청
                    case "OPW10004": if (paramArr.Length != 2) return FAIL; api.GetOPW10004(paramArr[0], paramArr[1]); break;
                    //OPW20001 기능명:선물옵션청산주문위탁증거금가계산요청
                    case "OPW20001": if (paramArr.Length != 7) return FAIL; api.GetOPW20001(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5], paramArr[6]); break;
                    //OPW20002 기능명:선옵당일매매변동현황요청
                    case "OPW20002": if (paramArr.Length != 5) return FAIL; api.GetOPW20002(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4]); break;
                    //OPW20003 기능명:선옵기간손익조회요청
                    case "OPW20003": if (paramArr.Length != 6) return FAIL; api.GetOPW20003(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5]); break;
                    //OPW20004 기능명:선옵주문체결내역상세요청
                    case "OPW20004": if (paramArr.Length != 8) return FAIL; api.GetOPW20004(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5], paramArr[6], paramArr[7]); break;
                    //OPW20005 기능명:선옵주문체결내역상세평균가요청
                    case "OPW20005": if (paramArr.Length != 8) return FAIL; api.GetOPW20005(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5], paramArr[6], paramArr[7]); break;
                    //OPW20006 기능명:선옵잔고상세현황요청
                    case "OPW20006": if (paramArr.Length != 4) return FAIL; api.GetOPW20006(paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPW20007 기능명:선옵잔고현황정산가기준요청
                    case "OPW20007": if (paramArr.Length != 3) return FAIL; api.GetOPW20007(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPW20008 기능명:계좌별결제예상내역조회요청
                    case "OPW20008": if (paramArr.Length != 3) return FAIL; api.GetOPW20008(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPW20009 기능명:선옵계좌별주문가능수량요청
                    case "OPW20009": if (paramArr.Length != 7) return FAIL; api.GetOPW20009(paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5], paramArr[6]); break;
                    //OPW20010 기능명:선옵예탁금및증거금조회요청
                    case "OPW20010": if (paramArr.Length != 3) return FAIL; api.GetOPW20010(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPW20011 기능명:선옵계좌예비증거금상세요청
                    case "OPW20011": if (paramArr.Length != 4) return FAIL; api.GetOPW20011(paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPW20012 기능명:선옵증거금상세내역요청
                    case "OPW20012": if (paramArr.Length != 3) return FAIL; api.GetOPW20012(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPW20013 기능명:계좌미결제청산가능수량조회요청
                    case "OPW20013": if (paramArr.Length != 4) return FAIL; api.GetOPW20013(paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPW20014 기능명:선옵실시간증거금산출요청
                    case "OPW20014": if (paramArr.Length != 4) return FAIL; api.GetOPW20014(paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPW20015 기능명:옵션매도주문증거금현황요청
                    case "OPW20015": if (paramArr.Length != 2) return FAIL; api.GetOPW20015(paramArr[0], paramArr[1]); break;
                    //OPW20016 기능명:신용융자 가능종목요청
                    case "OPW20016": if (paramArr.Length != 3) return FAIL; api.GetOPW20016(paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPW20017 기능명:신용융자 가능문의
                    case "OPW20017": if (paramArr.Length != 1) return FAIL; api.GetOPW20017(paramArr[0]); break;

                    default: return FAIL;
                }
                return SUCCESS;
            } catch {
                return FAIL;
            }
        }
    }
}
