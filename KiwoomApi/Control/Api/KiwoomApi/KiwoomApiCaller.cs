using KiwoomApi.Model.Def;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiwoomApi.Control.Api.KiwoomApi
{
    static class KiwoomApiCaller
    {
        private static readonly string SUCCESS = "SUCCESS";
        private static readonly string FAIL = "FAIL";

        private static readonly KiwoomApiController api = KiwoomApiController.Instance;


        /**
         * 
         */
        public static string Order(string callbackID , string orderMessage)
        {
            ///<param name="arg1">계좌번호 : 계좌번호10자리</param>
            ///<param name="arg2">주문유형 : 1:신규매수, 2:신규매도 3:매수취소, 4:매도취소, 5:매수정정, 6:매도정정</param>
            ///<param name="arg3">종목코드 : 종목코드</param>
            ///<param name="arg4">주문수량 : 주문수량</param>
            ///<param name="arg5">주문가격 : 주문가격</param>
            ///<param name="arg6">호가구분 : 00:지정가,03:시장가,05:조건부지정가,06:최유리지정가,07:최우선지정가,10:지정가IOC,13:시장가IOC,16:최유리IOC,20:지정가FOK,23:시장가FOK,26:최유리FOK,61:장전시간외종가,62:시간외단일가매매,81:장후시간외종가</param>
            ///<param name="arg7">원주문번호 : 신규주문에는 공백, 정정(취소)주문할 원주문번호를 입력합니다.</param>
            Console.WriteLine("Order:" + orderMessage);
            string[] paramArr = Separator.ParseData(orderMessage);
            //4550089011|1|048550|1|1730|03|
            api.GetORD10001(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5], paramArr[6]);
            return SUCCESS;
        }


        public static string CallTR(string trCode , string callbackID, string trMessage)
        {
            Console.WriteLine("CallTR:" + trCode + "," + callbackID + "," );
            Console.WriteLine("trMessage:" + trMessage);
            string[] paramArr = Separator.ParseData(trMessage);
            try
            {
                switch (trCode)
                {
                    //OPT10001 기능명:주식기본정보요청
                    case "OPT10001":  api.GetOPT10001(callbackID,paramArr[0]); break;
                    //OPT10002 기능명:주식거래원요청
                    case "OPT10002":  api.GetOPT10002(callbackID,paramArr[0]); break;
                    //OPT10003 기능명:체결정보요청
                    case "OPT10003":  api.GetOPT10003(callbackID,paramArr[0]); break;
                    //OPT10004 기능명:주식호가요청
                    case "OPT10004":  api.GetOPT10004(callbackID,paramArr[0]); break;
                    //OPT10005 기능명:주식일주월시분요청
                    case "OPT10005":  api.GetOPT10005(callbackID,paramArr[0]); break;
                    //OPT10006 기능명:주식시분요청
                    case "OPT10006":  api.GetOPT10006(callbackID,paramArr[0]); break;
                    //OPT10007 기능명:시세표성정보요청
                    case "OPT10007":  api.GetOPT10007(callbackID,paramArr[0]); break;
                    //OPT10008 기능명:주식외국인요청
                    case "OPT10008":  api.GetOPT10008(callbackID,paramArr[0], paramArr[1]); break;
                    //OPT10009 기능명:주식기관요청
                    case "OPT10009":  api.GetOPT10009(callbackID,paramArr[0]); break;
                    //OPT10010 기능명:업종프로그램요청
                    case "OPT10010":  api.GetOPT10010(callbackID,paramArr[0]); break;
                    //OPT10012 기능명:주문체결요청
                    case "OPT10012":  api.GetOPT10012(callbackID,paramArr[0]); break;
                    //OPT10013 기능명:신용매매동향요청
                    case "OPT10013":  api.GetOPT10013(callbackID,paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT10014 기능명:공매도추이요청
                    case "OPT10014":  api.GetOPT10014(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT10015 기능명:일별거래상세요청
                    case "OPT10015":  api.GetOPT10015(callbackID,paramArr[0], paramArr[1]); break;
                    //OPT10016 기능명:신고저가요청
                    case "OPT10016":  api.GetOPT10016(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5], paramArr[6], paramArr[7]); break;
                    //OPT10017 기능명:상하한가요청
                    case "OPT10017":  api.GetOPT10017(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5], paramArr[6]); break;
                    //OPT10018 기능명:고저가근접요청
                    case "OPT10018":  api.GetOPT10018(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5]); break;
                    //OPT10019 기능명:가격급등락요청
                    case "OPT10019":  api.GetOPT10019(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5], paramArr[6], paramArr[7], paramArr[8]); break;
                    //OPT10020 기능명:호가잔량상위요청
                    case "OPT10020":  api.GetOPT10020(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4]); break;
                    //OPT10021 기능명:호가잔량급증요청
                    case "OPT10021":  api.GetOPT10021(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5]); break;
                    //OPT10022 기능명:잔량율급증요청
                    case "OPT10022":  api.GetOPT10022(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4]); break;
                    //OPT10023 기능명:거래량급증요청
                    case "OPT10023":  api.GetOPT10023(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5], paramArr[6]); break;
                    //OPT10024 기능명:거래량갱신요청
                    case "OPT10024":  api.GetOPT10024(callbackID,paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT10025 기능명:매물대집중요청
                    case "OPT10025":  api.GetOPT10025(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4]); break;
                    //OPT10026 기능명:고저PER요청
                    case "OPT10026":  api.GetOPT10026(callbackID,paramArr[0]); break;
                    //OPT10027 기능명:전일대비등락률상위요청
                    case "OPT10027":  api.GetOPT10027(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5], paramArr[6], paramArr[7]); break;
                    //OPT10028 기능명:시가대비등락률요청
                    case "OPT10028":  api.GetOPT10028(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5], paramArr[6], paramArr[7]); break;
                    //OPT10029 기능명:예상체결등락률상위요청
                    case "OPT10029":  api.GetOPT10029(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5]); break;
                    //OPT10030 기능명:당일거래량상위요청
                    case "OPT10030":  api.GetOPT10030(callbackID,paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT10031 기능명:전일거래량상위요청
                    case "OPT10031":  api.GetOPT10031(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT10032 기능명:거래대금상위요청
                    case "OPT10032":  api.GetOPT10032(callbackID,paramArr[0], paramArr[1]); break;
                    //OPT10033 기능명:신용비율상위요청
                    case "OPT10033":  api.GetOPT10033(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4]); break;
                    //OPT10034 기능명:외인기간별매매상위요청
                    case "OPT10034":  api.GetOPT10034(callbackID,paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT10035 기능명:외인연속순매매상위요청
                    case "OPT10035":  api.GetOPT10035(callbackID,paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT10036 기능명:매매상위요청
                    case "OPT10036":  api.GetOPT10036(callbackID,paramArr[0], paramArr[1]); break;
                    //OPT10037 기능명:외국계창구매매상위요청
                    case "OPT10037":  api.GetOPT10037(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5]); break;
                    //OPT10038 기능명:종목별증권사순위요청
                    case "OPT10038":  api.GetOPT10038(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4]); break;
                    //OPT10039 기능명:증권사별매매상위요청
                    case "OPT10039":  api.GetOPT10039(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT10040 기능명:당일주요거래원요청
                    case "OPT10040":  api.GetOPT10040(callbackID,paramArr[0]); break;
                    //OPT10041 기능명:조기종료통화단위요청
                    case "OPT10041":  api.GetOPT10041(callbackID,paramArr[0], paramArr[1]); break;
                    //OPT10042 기능명:순매수거래원순위요청
                    case "OPT10042":  api.GetOPT10042(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5], paramArr[6]); break;
                    //OPT10043 기능명:거래원매물대분석요청
                    case "OPT10043":  api.GetOPT10043(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5], paramArr[6], paramArr[7]); break;
                    //OPT10044 기능명:일별기관매매종목요청
                    case "OPT10044":  api.GetOPT10044(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT10045 기능명:종목별기관매매추이요청
                    case "OPT10045": api.GetOPT10045(callbackID, paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5], paramArr[6]); break;
                    //OPT10047 기능명:체결강도추이일별요청
                    case "OPT10047": api.GetOPT10047(callbackID, paramArr[0], paramArr[1]); break;
                    //OPT10048 기능명:ELW일별민감도지표요청
                    case "OPT10048":  api.GetOPT10048(callbackID,paramArr[0]); break;
                    //OPT10049 기능명:ELW투자지표요청
                    case "OPT10049":  api.GetOPT10049(callbackID,paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT10050 기능명:ELW민감도지표요청
                    case "OPT10050":  api.GetOPT10050(callbackID,paramArr[0]); break;
                    //OPT10051 기능명:업종별투자자순매수요청
                    case "OPT10051":  api.GetOPT10051(callbackID,paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT10052 기능명:거래원순간거래량요청
                    case "OPT10052":  api.GetOPT10052(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT10053 기능명:당일상위이탈원요청
                    case "OPT10053":  api.GetOPT10053(callbackID,paramArr[0]); break;
                    //OPT10054 기능명:변동성완화장치발동종목요청
                    case "OPT10054":  api.GetOPT10054(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5], paramArr[6], paramArr[7], paramArr[8], paramArr[9], paramArr[10]); break;
                    //OPT10055 기능명:당일전일체결대량요청
                    case "OPT10055":  api.GetOPT10055(callbackID,paramArr[0], paramArr[1]); break;
                    //OPT10058 기능명:투자자별일별매매종목요청
                    case "OPT10058":  api.GetOPT10058(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4]); break;
                    //OPT10059 기능명:종목별투자자기관별요청
                    case "OPT10059":  api.GetOPT10059(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4]); break;
                    //OPT10060 기능명:종목별투자자기관별차트요청
                    case "OPT10060":  api.GetOPT10060(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4]); break;
                    //OPT10061 기능명:종목별투자자기관별합계요청
                    case "OPT10061":  api.GetOPT10061(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5]); break;
                    //OPT10062 기능명:동일순매매순위요청
                    case "OPT10062":  api.GetOPT10062(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5]); break;
                    //OPT10063 기능명:장중투자자별매매요청
                    case "OPT10063":  api.GetOPT10063(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4]); break;
                    //OPT10064 기능명:장중투자자별매매차트요청
                    case "OPT10064":  api.GetOPT10064(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT10065 기능명:장중투자자별매매상위요청
                    case "OPT10065":  api.GetOPT10065(callbackID,paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT10066 기능명:장중투자자별매매차트요청
                    case "OPT10066":  api.GetOPT10066(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT10067 기능명:대차거래내역요청
                    case "OPT10067":  api.GetOPT10067(callbackID,paramArr[0], paramArr[1]); break;
                    //OPT10068 기능명:대차거래추이요청
                    case "OPT10068":  api.GetOPT10068(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT10069 기능명:대차거래상위10종목요청
                    case "OPT10069":  api.GetOPT10069(callbackID,paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT10070 기능명:당일주요거래원요청
                    case "OPT10070":  api.GetOPT10070(callbackID,paramArr[0]); break;
                    //OPT10071 기능명:시간대별전일비거래비중요청
                    case "OPT10071":  api.GetOPT10071(callbackID,paramArr[0], paramArr[1]); break;
                    //OPT10072 기능명:일자별종목별실현손익요청
                    case "OPT10072":  api.GetOPT10072(callbackID,paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT10073 기능명:일자별종목별실현손익요청
                    case "OPT10073":  api.GetOPT10073(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT10074 기능명:일자별실현손익요청
                    case "OPT10074":  api.GetOPT10074(callbackID,paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT10075 기능명:실시간미체결요청
                    case "OPT10075":  api.GetOPT10075(callbackID,paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT10076 기능명:실시간체결요청
                    case "OPT10076":  api.GetOPT10076(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5]); break;
                    //OPT10077 기능명:당일실현손익상세요청
                    case "OPT10077":  api.GetOPT10077(callbackID,paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT10078 기능명:증권사별종목매매동향요청
                    case "OPT10078":  api.GetOPT10078(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT10079 기능명:주식틱차트조회요청
                    case "OPT10079":  api.GetOPT10079(callbackID,paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT10080 기능명:주식분봉차트조회요청
                    case "OPT10080":  api.GetOPT10080(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT10081 기능명:주식일봉차트조회요청
                    case "OPT10081":  api.GetOPT10081(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT10082 기능명:주식주봉차트조회요청
                    case "OPT10082":  api.GetOPT10082(callbackID,paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT10083 기능명:주식월봉차트조회요청
                    case "OPT10083":  api.GetOPT10083(callbackID,paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT10084 기능명:당일전일체결요청
                    case "OPT10084":  api.GetOPT10084(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT10085 기능명:계좌수익률요청
                    case "OPT10085":  api.GetOPT10085(callbackID,paramArr[0]); break;
                    //OPT10086 기능명:일별주가요청
                    case "OPT10086":  api.GetOPT10086(callbackID,paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT10087 기능명:시간외단일가요청
                    case "OPT10087":  api.GetOPT10087(callbackID,paramArr[0]); break;
                    //OPT10094 기능명:주식년봉차트조회요청
                    case "OPT10094":  api.GetOPT10094(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT20001 기능명:업종현재가요청
                    case "OPT20001":  api.GetOPT20001(callbackID,paramArr[0], paramArr[1]); break;
                    //OPT20002 기능명:업종별주가요청
                    case "OPT20002":  api.GetOPT20002(callbackID,paramArr[0], paramArr[1]); break;
                    //OPT20003 기능명:전업종지수요청
                    case "OPT20003":  api.GetOPT20003(callbackID,paramArr[0]); break;
                    //OPT20004 기능명:업종틱차트조회요청
                    case "OPT20004":  api.GetOPT20004(callbackID,paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT20005 기능명:업종분봉조회요청
                    case "OPT20005":  api.GetOPT20005(callbackID,paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT20006 기능명:업종일봉조회요청
                    case "OPT20006":  api.GetOPT20006(callbackID,paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT20007 기능명:업종주봉조회요청
                    case "OPT20007":  api.GetOPT20007(callbackID,paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT20008 기능명:업종월봉조회요청
                    case "OPT20008":  api.GetOPT20008(callbackID,paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT20009 기능명:업종현재가일별요청
                    case "OPT20009":  api.GetOPT20009(callbackID,paramArr[0], paramArr[1]); break;
                    //OPT20019 기능명:업종년봉조회요청
                    case "OPT20019":  api.GetOPT20019(callbackID,paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT20068 기능명:대차거래추이요청(종목별)
                    case "OPT20068":  api.GetOPT20068(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT30001 기능명:ELW가격급등락요청
                    case "OPT30001":  api.GetOPT30001(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5], paramArr[6], paramArr[7], paramArr[8]); break;
                    //OPT30002 기능명:거래원별ELW순매매상위요청
                    case "OPT30002":  api.GetOPT30002(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4]); break;
                    //OPT30003 기능명:ELW LP보유일별추이요청
                    case "OPT30003":  api.GetOPT30003(callbackID,paramArr[0], paramArr[1]); break;
                    //OPT30004 기능명:ELW괴리율요청
                    case "OPT30004":  api.GetOPT30004(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4]); break;
                    //OPT30005 기능명:ELW조건검색요청
                    case "OPT30005":  api.GetOPT30005(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4]); break;
                    //OPT30007 기능명:ELW종목상세요청
                    case "OPT30007":  api.GetOPT30007(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5]); break;
                    //OPT30008 기능명:ELW민감도지표요청
                    case "OPT30008":  api.GetOPT30008(callbackID,paramArr[0]); break;
                    //OPT30009 기능명:ELW등락율순위요청
                    case "OPT30009":  api.GetOPT30009(callbackID,paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT30010 기능명:ELW잔량순위요청
                    case "OPT30010":  api.GetOPT30010(callbackID,paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT30011 기능명:ELW근접율요청
                    case "OPT30011":  api.GetOPT30011(callbackID,paramArr[0]); break;
                    //OPT30012 기능명:ELW종목상세정보요청
                    case "OPT30012":  api.GetOPT30012(callbackID,paramArr[0]); break;
                    //OPT40001 기능명:ETF수익율요청
                    case "OPT40001":  api.GetOPT40001(callbackID,paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT40002 기능명:ETF종목정보요청
                    case "OPT40002":  api.GetOPT40002(callbackID,paramArr[0]); break;
                    //OPT40003 기능명:ETF일별추이요청
                    case "OPT40003":  api.GetOPT40003(callbackID,paramArr[0]); break;
                    //OPT40004 기능명:ETF전체시세요청
                    case "OPT40004":  api.GetOPT40004(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4]); break;
                    //OPT40005 기능명:ETF일별추이요청
                    case "OPT40005":  api.GetOPT40005(callbackID,paramArr[0]); break;
                    //OPT40006 기능명:ETF시간대별추이요청
                    case "OPT40006":  api.GetOPT40006(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4]); break;
                    //OPT40007 기능명:ETF시간대별체결요청
                    case "OPT40007":  api.GetOPT40007(callbackID,paramArr[0]); break;
                    //OPT40008 기능명:ETF시간대별체결요청
                    case "OPT40008":  api.GetOPT40008(callbackID,paramArr[0]); break;
                    //OPT40009 기능명:ETF시간대별체결요청
                    case "OPT40009":  api.GetOPT40009(callbackID,paramArr[0]); break;
                    //OPT40010 기능명:ETF시간대별추이요청
                    case "OPT40010":  api.GetOPT40010(callbackID,paramArr[0]); break;
                    //OPT50001 기능명:선옵현재가정보요청
                    case "OPT50001":  api.GetOPT50001(callbackID,paramArr[0]); break;
                    //OPT50002 기능명:선옵일자별체결요청
                    case "OPT50002":  api.GetOPT50002(callbackID,paramArr[0]); break;
                    //OPT50003 기능명:선옵시고저가요청
                    case "OPT50003":  api.GetOPT50003(callbackID,paramArr[0]); break;
                    //OPT50004 기능명:콜옵션행사가요청
                    case "OPT50004":  api.GetOPT50004(callbackID,paramArr[0]); break;
                    //OPT50005 기능명:선옵시간별거래량요청
                    case "OPT50005":  api.GetOPT50005(callbackID,paramArr[0]); break;
                    //OPT50006 기능명:선옵체결추이요청
                    case "OPT50006":  api.GetOPT50006(callbackID,paramArr[0]); break;
                    //OPT50007 기능명:선물시세추이요청
                    case "OPT50007":  api.GetOPT50007(callbackID,paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT50008 기능명:프로그램매매추이차트요청
                    case "OPT50008":  api.GetOPT50008(callbackID,paramArr[0], paramArr[1]); break;
                    //OPT50009 기능명:선옵시간별잔량요청
                    case "OPT50009":  api.GetOPT50009(callbackID,paramArr[0], paramArr[1]); break;
                    //OPT50010 기능명:선옵호가잔량추이요청
                    case "OPT50010":  api.GetOPT50010(callbackID,paramArr[0], paramArr[1]); break;
                    //OPT50011 기능명:선옵호가잔량추이요청
                    case "OPT50011":  api.GetOPT50011(callbackID,paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT50012 기능명:선옵타임스프레드차트요청
                    case "OPT50012":  api.GetOPT50012(callbackID,paramArr[0], paramArr[1]); break;
                    //OPT50013 기능명:선물가격대별비중차트요청
                    case "OPT50013":  api.GetOPT50013(callbackID,paramArr[0], paramArr[1]); break;
                    //OPT50014 기능명:선물가격대별비중차트요청
                    case "OPT50014":  api.GetOPT50014(callbackID,paramArr[0], paramArr[1]); break;
                    //OPT50015 기능명:선물미결제약정일차트요청
                    case "OPT50015":  api.GetOPT50015(callbackID,paramArr[0], paramArr[1]); break;
                    //OPT50016 기능명:베이시스추이차트요청
                    case "OPT50016":  api.GetOPT50016(callbackID,paramArr[0], paramArr[1]); break;
                    //OPT50017 기능명:베이시스추이차트요청
                    case "OPT50017":  api.GetOPT50017(callbackID,paramArr[0], paramArr[1]); break;
                    //OPT50018 기능명:풋콜옵션비율차트요청
                    case "OPT50018":  api.GetOPT50018(callbackID,paramArr[0]); break;
                    //OPT50019 기능명:선물옵션현재가정보요청
                    case "OPT50019":  api.GetOPT50019(callbackID,paramArr[0]); break;
                    //OPT50020 기능명:복수종목결제월별시세요청
                    case "OPT50020":  api.GetOPT50020(callbackID,paramArr[0]); break;
                    //OPT50021 기능명:콜종목결제월별시세요청
                    case "OPT50021":  api.GetOPT50021(callbackID,paramArr[0]); break;
                    //OPT50022 기능명:풋종목결제월별시세요청
                    case "OPT50022":  api.GetOPT50022(callbackID,paramArr[0]); break;
                    //OPT50023 기능명:민감도지표추이요청
                    case "OPT50023":  api.GetOPT50023(callbackID,paramArr[0], paramArr[1]); break;
                    //OPT50024 기능명:일별변동성분석그래프요청
                    case "OPT50024":  api.GetOPT50024(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT50025 기능명:시간별변동성분석그래프요청
                    case "OPT50025":  api.GetOPT50025(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT50026 기능명:선옵주문체결요청
                    case "OPT50026":  api.GetOPT50026(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT50027 기능명:선옵잔고요청
                    case "OPT50027":  api.GetOPT50027(callbackID,paramArr[0]); break;
                    //OPT50028 기능명:선물틱차트요청
                    case "OPT50028":  api.GetOPT50028(callbackID,paramArr[0], paramArr[1]); break;
                    //OPT50029 기능명:선물분차트요청
                    case "OPT50029":  api.GetOPT50029(callbackID,paramArr[0], paramArr[1]); break;
                    //OPT50030 기능명:선물일차트요청
                    case "OPT50030":  api.GetOPT50030(callbackID,paramArr[0], paramArr[1]); break;
                    //OPT50031 기능명:선옵잔고손익요청
                    case "OPT50031":  api.GetOPT50031(callbackID,paramArr[0]); break;
                    //OPT50032 기능명:선옵당일실현손익요청
                    case "OPT50032":  api.GetOPT50032(callbackID,paramArr[0], paramArr[1]); break;
                    //OPT50033 기능명:선옵잔존일조회요청
                    case "OPT50033":  api.GetOPT50033(callbackID,paramArr[0], paramArr[1]); break;
                    //OPT50034 기능명:선옵전일가격요청
                    case "OPT50034":  api.GetOPT50034(callbackID,paramArr[0], paramArr[1]); break;
                    //OPT50035 기능명:지수변동성차트요청
                    case "OPT50035":  api.GetOPT50035(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT50036 기능명:주요지수변동성차트요청
                    case "OPT50036":  api.GetOPT50036(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT50037 기능명:코스피200지수요청
                    case "OPT50037":  api.GetOPT50037(callbackID,paramArr[0], paramArr[1]); break;
                    //OPT50038 기능명:투자자별만기손익차트요청
                    case "OPT50038":  api.GetOPT50038(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT50040 기능명:선옵시고저가요청
                    case "OPT50040":  api.GetOPT50040(callbackID,paramArr[0]); break;
                    //OPT50043 기능명:주식선물거래량상위종목요청
                    case "OPT50043":  api.GetOPT50043(callbackID,paramArr[0]); break;
                    //OPT50044 기능명:주식선물시세표요청
                    case "OPT50044":  api.GetOPT50044(callbackID,paramArr[0]); break;
                    //OPT50062 기능명:선물미결제약정분차트요청
                    case "OPT50062":  api.GetOPT50062(callbackID,paramArr[0], paramArr[1]); break;
                    //OPT50063 기능명:옵션미결제약정일차트요청
                    case "OPT50063":  api.GetOPT50063(callbackID,paramArr[0], paramArr[1]); break;
                    //OPT50064 기능명:옵션미결제약정분차트요청
                    case "OPT50064":  api.GetOPT50064(callbackID,paramArr[0], paramArr[1]); break;
                    //OPT50065 기능명:풋옵션행사가요청
                    case "OPT50065":  api.GetOPT50065(callbackID,paramArr[0]); break;
                    //OPT50066 기능명:옵션틱차트요청
                    case "OPT50066":  api.GetOPT50066(callbackID,paramArr[0], paramArr[1]); break;
                    //OPT50067 기능명:옵션분차트요청
                    case "OPT50067":  api.GetOPT50067(callbackID,paramArr[0], paramArr[1]); break;
                    //OPT50068 기능명:옵션일차트요청
                    case "OPT50068":  api.GetOPT50068(callbackID,paramArr[0], paramArr[1]); break;
                    //OPT50071 기능명:선물주차트요청
                    case "OPT50071":  api.GetOPT50071(callbackID,paramArr[0], paramArr[1]); break;
                    //OPT50072 기능명:선물월차트요청
                    case "OPT50072":  api.GetOPT50072(callbackID,paramArr[0], paramArr[1]); break;
                    //OPT50073 기능명:선물년차트요청
                    case "OPT50073":  api.GetOPT50073(callbackID,paramArr[0], paramArr[1]); break;
                    //OPT90001 기능명:테마그룹별요청
                    case "OPT90001":  api.GetOPT90001(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4]); break;
                    //OPT90002 기능명:테마구성종목요청
                    case "OPT90002":  api.GetOPT90002(callbackID,paramArr[0], paramArr[1]); break;
                    //OPT90003 기능명:프로그램순매수상위50요청
                    case "OPT90003":  api.GetOPT90003(callbackID,paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT90004 기능명:종목별프로그램매매현황요청
                    case "OPT90004":  api.GetOPT90004(callbackID,paramArr[0], paramArr[1]); break;
                    //OPT90005 기능명:프로그램매매추이요청
                    case "OPT90005":  api.GetOPT90005(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4]); break;
                    //OPT90006 기능명:프로그램매매차익잔고추이요청
                    case "OPT90006":  api.GetOPT90006(callbackID,paramArr[0]); break;
                    //OPT90007 기능명:프로그램매매누적추이요청
                    case "OPT90007":  api.GetOPT90007(callbackID,paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPT90008 기능명:종목시간별프로그램매매추이요청
                    case "OPT90008":  api.GetOPT90008(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT90009 기능명:외국인기관매매상위요청
                    case "OPT90009":  api.GetOPT90009(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT90010 기능명:차익잔고현황요청
                    case "OPT90010":  api.GetOPT90010(callbackID,paramArr[0], paramArr[1]); break;
                    //OPT90011 기능명:차익잔고현황요청
                    case "OPT90011":  api.GetOPT90011(callbackID,paramArr[0], paramArr[1]); break;
                    //OPT90012 기능명:대차거래내역요청
                    case "OPT90012":  api.GetOPT90012(callbackID,paramArr[0], paramArr[1]); break;
                    //OPT90013 기능명:종목일별프로그램매매추이요청
                    case "OPT90013":  api.GetOPT90013(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPT99999 기능명:대차거래상위10종목요청
                    case "OPT99999":  api.GetOPT99999(callbackID,paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPTFOFID 기능명:선물전체시세요청
                    case "OPTFOFID":  api.GetOPTFOFID(callbackID,paramArr[0]); break;
                    //OPTKWFID 기능명:관심종목정보요청
                    case "OPTKWFID":  api.GetOPTKWFID(callbackID,paramArr[0]); break;
                    //OPTKWINV 기능명:관심종목투자자정보요청
                    case "OPTKWINV":  api.GetOPTKWINV(callbackID,paramArr[0]); break;
                    //OPTKWPRO 기능명:관심종목프로그램정보요청
                    case "OPTKWPRO":  api.GetOPTKWPRO(callbackID,paramArr[0]); break;
                    //OPW00001 기능명:예수금상세현황요청
                    case "OPW00001":  api.GetOPW00001(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPW00002 기능명:일별추정예탁자산현황요청
                    case "OPW00002":  api.GetOPW00002(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPW00003 기능명:추정자산조회요청
                    case "OPW00003":  api.GetOPW00003(callbackID,paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPW00004 기능명:계좌평가현황요청
                    case "OPW00004":  api.GetOPW00004(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPW00005 기능명:체결잔고요청
                    case "OPW00005":  api.GetOPW00005(callbackID,paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPW00006 기능명:관리자별주문체결내역요청
                    case "OPW00006":  api.GetOPW00006(callbackID,paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPW00007 기능명:계좌별주문체결내역상세요청
                    case "OPW00007":  api.GetOPW00007(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5], paramArr[6], paramArr[7], paramArr[8]); break;
                    //OPW00008 기능명:계좌별익일결제예정내역요청
                    case "OPW00008":  api.GetOPW00008(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPW00009 기능명:계좌별주문체결현황요청
                    case "OPW00009":  api.GetOPW00009(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5], paramArr[6], paramArr[7], paramArr[8], paramArr[9]); break;
                    //OPW00010 기능명:주문인출가능금액요청
                    case "OPW00010":  api.GetOPW00010(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5], paramArr[6], paramArr[7], paramArr[8]); break;
                    //OPW00011 기능명:증거금율별주문가능수량조회요청
                    case "OPW00011":  api.GetOPW00011(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4]); break;
                    //OPW00012 기능명:신용보증금율별주문가능수량조회요청
                    case "OPW00012":  api.GetOPW00012(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4]); break;
                    //OPW00013 기능명:증거금세부내역조회요청
                    case "OPW00013":  api.GetOPW00013(callbackID,paramArr[0], paramArr[1]); break;
                    //OPW00014 기능명:비밀번호일치여부요청
                    case "OPW00014":  api.GetOPW00014(callbackID,paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPW00015 기능명:위탁종합거래내역요청
                    case "OPW00015":  api.GetOPW00015(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5], paramArr[6], paramArr[7], paramArr[8], paramArr[9]); break;
                    //OPW00016 기능명:일별계좌수익률상세현황요청
                    case "OPW00016":  api.GetOPW00016(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4]); break;
                    //OPW00017 기능명:계좌별당일현황요청
                    case "OPW00017":  api.GetOPW00017(callbackID,paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPW00018 기능명:계좌평가잔고내역요청
                    case "OPW00018":  api.GetOPW00018(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPW10001 기능명:ELW종목별민감도지표요청
                    case "OPW10001":  api.GetOPW10001(callbackID,paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPW10002 기능명:ELW투자지표요청
                    case "OPW10002":  api.GetOPW10002(callbackID,paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPW10003 기능명:ELW민감도지표요청
                    case "OPW10003":  api.GetOPW10003(callbackID,paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPW10004 기능명:업종별순매수요청
                    case "OPW10004":  api.GetOPW10004(callbackID,paramArr[0], paramArr[1]); break;
                    //OPW20001 기능명:선물옵션청산주문위탁증거금가계산요청
                    case "OPW20001":  api.GetOPW20001(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5], paramArr[6]); break;
                    //OPW20002 기능명:선옵당일매매변동현황요청
                    case "OPW20002":  api.GetOPW20002(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4]); break;
                    //OPW20003 기능명:선옵기간손익조회요청
                    case "OPW20003":  api.GetOPW20003(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5]); break;
                    //OPW20004 기능명:선옵주문체결내역상세요청
                    case "OPW20004":  api.GetOPW20004(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5], paramArr[6], paramArr[7]); break;
                    //OPW20005 기능명:선옵주문체결내역상세평균가요청
                    case "OPW20005":  api.GetOPW20005(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5], paramArr[6], paramArr[7]); break;
                    //OPW20006 기능명:선옵잔고상세현황요청
                    case "OPW20006":  api.GetOPW20006(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPW20007 기능명:선옵잔고현황정산가기준요청
                    case "OPW20007":  api.GetOPW20007(callbackID,paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPW20008 기능명:계좌별결제예상내역조회요청
                    case "OPW20008":  api.GetOPW20008(callbackID,paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPW20009 기능명:선옵계좌별주문가능수량요청
                    case "OPW20009":  api.GetOPW20009(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3], paramArr[4], paramArr[5], paramArr[6]); break;
                    //OPW20010 기능명:선옵예탁금및증거금조회요청
                    case "OPW20010":  api.GetOPW20010(callbackID,paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPW20011 기능명:선옵계좌예비증거금상세요청
                    case "OPW20011":  api.GetOPW20011(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPW20012 기능명:선옵증거금상세내역요청
                    case "OPW20012":  api.GetOPW20012(callbackID,paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPW20013 기능명:계좌미결제청산가능수량조회요청
                    case "OPW20013":  api.GetOPW20013(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPW20014 기능명:선옵실시간증거금산출요청
                    case "OPW20014":  api.GetOPW20014(callbackID,paramArr[0], paramArr[1], paramArr[2], paramArr[3]); break;
                    //OPW20015 기능명:옵션매도주문증거금현황요청
                    case "OPW20015":  api.GetOPW20015(callbackID,paramArr[0], paramArr[1]); break;
                    //OPW20016 기능명:신용융자 가능종목요청
                    case "OPW20016":  api.GetOPW20016(callbackID,paramArr[0], paramArr[1], paramArr[2]); break;
                    //OPW20017 기능명:신용융자 가능문의
                    case "OPW20017":  api.GetOPW20017(callbackID,paramArr[0]); break;

                    default: return FAIL;
                }
                return SUCCESS;
            } catch {
                return FAIL;
            }
        }
    }
}
