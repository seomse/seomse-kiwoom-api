﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KiwoomApi.Control.Socket;
using KiwoomCode;

namespace KiwoomApi.Control.Api.KiwoomApi
{
    class KiwoomApiController
    {
        Logger<KiwoomApiController> logger = new Logger<KiwoomApiController>();
        public AxKHOpenAPILib.AxKHOpenAPI AxKHOpenAPI { get; set; }

        public Boolean IsReady { get; set; }
        private int _scrNum = 1;

        private readonly string PARAM_SEPARATOR = ",";
        private readonly string DATA_SEPARATOR = "|";

        private string nowCallbackID;

        #region SingleTon
        private class Holder { internal static readonly KiwoomApiController INSTANCE = new KiwoomApiController(); }
        public static KiwoomApiController Instance { get { return Holder.INSTANCE; } }
        private KiwoomApiController()
        {
            IsReady = false;
        }
        #endregion

        #region Controll Functions

        // 화면번호 생산
        private string GetScrNum()
        {
            if (_scrNum < 200)
                _scrNum++;
            else
                _scrNum = 1;

            return _scrNum.ToString();
        }
        // 실시간 연결 종료
        private void DisconnectAllRealData()
        {
            for (int i = _scrNum; i > 5000; i--)
            {
                AxKHOpenAPI.DisconnectRealData(i.ToString());
            }

            _scrNum = 5000;
        }


        private void SetInputValue(string code, string value)
        {
            AxKHOpenAPI.SetInputValue(code, value);
        }

        private void CommRqData(string RQName, string TRcode, int seq)
        {
            CommRqData(RQName, TRcode, seq, GetScrNum());
        }

        private void CommRqData(string RQName, string TRcode, int seq, string screenNum)
        {
            AxKHOpenAPI.CommRqData(RQName, TRcode, seq, screenNum);
        }

        private void CommKwRqData(string codeListStr, string screenNum)
        {
            int nCodeCount = codeListStr.Split(';').Length;
            AxKHOpenAPI.CommKwRqData(codeListStr, 0, nCodeCount, 0, "관심종목정보", screenNum);
        }


        public void Init()
        {
            if (AxKHOpenAPI == null)
            {
                logger.Info("Init AxKHOpenAPI first. exit.");
                return;
            }
            AxKHOpenAPI.OnReceiveTrData += new AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEventHandler(axKHOpenAPI_OnReceiveTrData);
            AxKHOpenAPI.OnReceiveRealData += new AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealDataEventHandler(axKHOpenAPI_OnReceiveRealData);
            AxKHOpenAPI.OnReceiveMsg += new AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveMsgEventHandler(axKHOpenAPI_OnReceiveMsg);
            AxKHOpenAPI.OnReceiveChejanData += new AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveChejanDataEventHandler(axKHOpenAPI_OnReceiveChejanData);
            AxKHOpenAPI.OnEventConnect += new AxKHOpenAPILib._DKHOpenAPIEvents_OnEventConnectEventHandler(axKHOpenAPI_OnEventConnect);
            AxKHOpenAPI.OnReceiveInvestRealData += new AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveInvestRealDataEventHandler(axKHOpenAPI_OnReceiveInvestRealData);
            AxKHOpenAPI.OnReceiveRealCondition += new AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealConditionEventHandler(axKHOpenAPI_OnReceiveRealCondition);
            AxKHOpenAPI.OnReceiveTrCondition += new AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrConditionEventHandler(axKHOpenAPI_OnReceiveTrCondition);
            AxKHOpenAPI.OnReceiveConditionVer += new AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveConditionVerEventHandler(axKHOpenAPI_OnReceiveConditionVer);
            GetLGI10001();
        }

        #endregion

        #region CallBack Events

        private void axKHOpenAPI_OnEventConnect(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnEventConnectEvent e)
        {
            if (Error.IsError(e.nErrCode))
            {
                logger.Info("[로그인 처리결과] " + Error.GetErrorMessage());
            }
            else
            {
                logger.Info("[로그인 처리결과] " + Error.GetErrorMessage());
            }

        }

        private void axKHOpenAPI_OnReceiveChejanData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveChejanDataEvent e)
        {
            //onReceiveChejanData(sender, e);
            logger.Info("OnReceiveChejanData:" + e.sGubun );
        }

        private void axKHOpenAPI_OnReceiveConditionVer(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveConditionVerEvent e)
        {
            logger.Info("axKHOpenAPI_OnReceiveConditionVer " + e.sMsg) ;
            //onReceiveConditionVer(sender, e);
        }

        private void axKHOpenAPI_OnReceiveInvestRealData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveInvestRealDataEvent e)
        {
            logger.Info("axKHOpenAPI_OnReceiveInvestRealData " + e.sRealKey) ;
            //onReceiveInvestRealData(sender, e);
        }

        private void axKHOpenAPI_OnReceiveMsg(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveMsgEvent e)
        {
            
            logger.Info("axKHOpenAPI_OnReceiveMsg " + e.sMsg) ;
            //onReceiveMsg(sender, e);
        }

        private void axKHOpenAPI_OnReceiveRealCondition(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealConditionEvent e)
        {
            logger.Info("axKHOpenAPI_OnReceiveRealCondition " + e.strConditionName) ;
            //onReceiveRealCondition(sender, e);
        }

        private void axKHOpenAPI_OnReceiveRealData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealDataEvent e)
        {
            logger.Info("axKHOpenAPI_OnReceiveRealData " + e.sRealData) ;
            //onReceiveRealData(sender, e);
        }

        private void axKHOpenAPI_OnReceiveTrCondition(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrConditionEvent e)
        {
            
            logger.Info("axKHOpenAPI_OnReceiveTrCondition " + e.strConditionName) ;
            //onReceiveTrCondition(sender, e);
        }
        /// <summary>
        /// TR 콜백 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void axKHOpenAPI_OnReceiveTrData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e)
        {
            StringBuilder apiMessage = new StringBuilder();

            object result = AxKHOpenAPI.GetCommDataEx(e.sTrCode, e.sRQName);
            string code = getTRCode(e.sRQName);

            if (result == null)
            {
                logger.Err(" Data is null!");

                apiMessage.Append(code).Append(PARAM_SEPARATOR)
                    .Append(nowCallbackID).Append(PARAM_SEPARATOR)
                    .Append("0").Append(PARAM_SEPARATOR).Append("FAIL");

                ApiSocketClient.Instance.SendMessage("KWCBTR01", apiMessage.ToString());
                return;
            }

            Type valueType = result.GetType();
            object[,] resultArrMulti = null;
            object[] resultArr = null ;
           
            logger.Info("axKHOpenAPI_OnReceiveTrData:" + e.sRQName);
            logger.Info("valueType.FullName " + valueType.FullName);

            if (valueType.FullName == "System.Object[,]")
            {

                resultArrMulti = (object[,])result;

                if (resultArrMulti.Length == 0)
                {
                    try
                    {

                        resultArr = (object[])result;
                    }
                    catch (Exception ex)
                    {
                        apiMessage.Append(code).Append(PARAM_SEPARATOR)
                       .Append(nowCallbackID).Append(PARAM_SEPARATOR)
                       .Append(e.sPrevNext).Append(PARAM_SEPARATOR);

                        ApiSocketClient.Instance.SendMessage("KWCBTR01", apiMessage.ToString());
                        return;

                    }

                    // logger.Info("apiMessage: " + apiMessage + " " + resultArr.Length +" " + resultArr.GetLength(0));
                    apiMessage.Append(code).Append(PARAM_SEPARATOR)
                        .Append(nowCallbackID).Append(PARAM_SEPARATOR)
                        .Append(e.sPrevNext).Append(PARAM_SEPARATOR);
                    for (int i = 0; i < resultArr.GetLength(0); i++)
                    {
                        apiMessage.Append(resultArr[i]).Append(DATA_SEPARATOR);
                    }
                    apiMessage.Length = apiMessage.Length - 1;
                }
                else
                {
                    // logger.Info("apiMessage: if else!");
                    apiMessage.Append(code).Append(PARAM_SEPARATOR)
                        .Append(nowCallbackID).Append(PARAM_SEPARATOR)
                        .Append(e.sPrevNext).Append(PARAM_SEPARATOR);

                    // logger.Info("apiMessage: " + apiMessage + " " + resultArrMulti.Length);

                    for (int i = 0; i < resultArrMulti.GetLength(0); i++)
                    {
                        for (int j = 0; j < resultArrMulti.GetLength(1); j++)
                        {
                            apiMessage.Append(resultArrMulti[i, j]).Append(DATA_SEPARATOR);
                        }
                        apiMessage.Length = apiMessage.Length - 1;
                        apiMessage.Append("\n");
                    }
                    if (apiMessage.Length > 0)
                    {
                        apiMessage.Length = apiMessage.Length - 1;
                    }
                }
            }
            else if (valueType.FullName == "System.Object[]")
            {
                logger.Info("apiMessage: else if!");
                resultArr = (object[])result;
                apiMessage.Append(code).Append(PARAM_SEPARATOR)
                    .Append(nowCallbackID).Append(PARAM_SEPARATOR)
                    .Append(e.sPrevNext).Append(PARAM_SEPARATOR);
                for (int i = 0; i < resultArr.GetLength(0); i++)
                {
                    apiMessage.Append(resultArr[i]).Append(DATA_SEPARATOR);
                }
                apiMessage.Length = apiMessage.Length - 1;
            }
            else
            {
                logger.Info("apiMessage: else!" );
                apiMessage.Append(result);
            }

            ApiSocketClient.Instance.SendMessage("KWCBTR01", apiMessage.ToString()); 
        }

        private string getTRCode(string sRQName)
        {
            String code = "FAIL";
            if (sRQName == "주식기본정보요청") code = "OPT10001";
            else if (sRQName == "주식거래원요청") code = "OPT10002";
            else if (sRQName == "체결정보요청") code = "OPT10003";
            else if (sRQName == "주식호가요청") code = "OPT10004";
            else if (sRQName == "주식일주월시분요청") code = "OPT10005";
            else if (sRQName == "주식시분요청") code = "OPT10006";
            else if (sRQName == "시세표성정보요청") code = "OPT10007";
            else if (sRQName == "주식외국인요청") code = "OPT10008";
            else if (sRQName == "주식기관요청") code = "OPT10009";
            else if (sRQName == "업종프로그램요청") code = "OPT10010";
            else if (sRQName == "주문체결요청") code = "OPT10012";
            else if (sRQName == "신용매매동향요청") code = "OPT10013";
            else if (sRQName == "공매도추이요청") code = "OPT10014";
            else if (sRQName == "일별거래상세요청") code = "OPT10015";
            else if (sRQName == "신고저가요청") code = "OPT10016";
            else if (sRQName == "상하한가요청") code = "OPT10017";
            else if (sRQName == "고저가근접요청") code = "OPT10018";
            else if (sRQName == "가격급등락요청") code = "OPT10019";
            else if (sRQName == "호가잔량상위요청") code = "OPT10020";
            else if (sRQName == "호가잔량급증요청") code = "OPT10021";
            else if (sRQName == "잔량율급증요청") code = "OPT10022";
            else if (sRQName == "거래량급증요청") code = "OPT10023";
            else if (sRQName == "거래량갱신요청") code = "OPT10024";
            else if (sRQName == "매물대집중요청") code = "OPT10025";
            else if (sRQName == "고저PER요청") code = "OPT10026";
            else if (sRQName == "전일대비등락률상위요청") code = "OPT10027";
            else if (sRQName == "시가대비등락률요청") code = "OPT10028";
            else if (sRQName == "예상체결등락률상위요청") code = "OPT10029";
            else if (sRQName == "당일거래량상위요청") code = "OPT10030";
            else if (sRQName == "전일거래량상위요청") code = "OPT10031";
            else if (sRQName == "거래대금상위요청") code = "OPT10032";
            else if (sRQName == "신용비율상위요청") code = "OPT10033";
            else if (sRQName == "외인기간별매매상위요청") code = "OPT10034";
            else if (sRQName == "외인연속순매매상위요청") code = "OPT10035";
            else if (sRQName == "매매상위요청") code = "OPT10036";
            else if (sRQName == "외국계창구매매상위요청") code = "OPT10037";
            else if (sRQName == "종목별증권사순위요청") code = "OPT10038";
            else if (sRQName == "증권사별매매상위요청") code = "OPT10039";
            else if (sRQName == "당일주요거래원요청") code = "OPT10040";
            else if (sRQName == "조기종료통화단위요청") code = "OPT10041";
            else if (sRQName == "순매수거래원순위요청") code = "OPT10042";
            else if (sRQName == "거래원매물대분석요청") code = "OPT10043";
            else if (sRQName == "일별기관매매종목요청") code = "OPT10044";
            else if (sRQName == "종목별기관매매추이요청") code = "OPT10045";
            else if (sRQName == "체결강도추이일별요청") code = "OPT10047";
            else if (sRQName == "ELW일별민감도지표요청") code = "OPT10048";
            else if (sRQName == "ELW투자지표요청") code = "OPT10049";
            else if (sRQName == "ELW민감도지표요청") code = "OPT10050";
            else if (sRQName == "업종별투자자순매수요청") code = "OPT10051";
            else if (sRQName == "거래원순간거래량요청") code = "OPT10052";
            else if (sRQName == "당일상위이탈원요청") code = "OPT10053";
            else if (sRQName == "변동성완화장치발동종목요청") code = "OPT10054";
            else if (sRQName == "당일전일체결대량요청") code = "OPT10055";
            else if (sRQName == "투자자별일별매매종목요청") code = "OPT10058";
            else if (sRQName == "종목별투자자기관별요청") code = "OPT10059";
            else if (sRQName == "종목별투자자기관별차트요청") code = "OPT10060";
            else if (sRQName == "종목별투자자기관별합계요청") code = "OPT10061";
            else if (sRQName == "동일순매매순위요청") code = "OPT10062";
            else if (sRQName == "장중투자자별매매요청") code = "OPT10063";
            else if (sRQName == "장중투자자별매매차트요청") code = "OPT10064";
            else if (sRQName == "장중투자자별매매상위요청") code = "OPT10065";
            else if (sRQName == "장중투자자별매매차트요청") code = "OPT10066";
            else if (sRQName == "대차거래내역요청") code = "OPT10067";
            else if (sRQName == "대차거래추이요청") code = "OPT10068";
            else if (sRQName == "대차거래상위10종목요청") code = "OPT10069";
            else if (sRQName == "당일주요거래원요청") code = "OPT10070";
            else if (sRQName == "시간대별전일비거래비중요청") code = "OPT10071";
            else if (sRQName == "일자별종목별실현손익요청") code = "OPT10072";
            else if (sRQName == "일자별종목별실현손익요청") code = "OPT10073";
            else if (sRQName == "일자별실현손익요청") code = "OPT10074";
            else if (sRQName == "실시간미체결요청") code = "OPT10075";
            else if (sRQName == "실시간체결요청") code = "OPT10076";
            else if (sRQName == "당일실현손익상세요청") code = "OPT10077";
            else if (sRQName == "증권사별종목매매동향요청") code = "OPT10078";
            else if (sRQName == "주식틱차트조회요청") code = "OPT10079";
            else if (sRQName == "주식분봉차트조회요청") code = "OPT10080";
            else if (sRQName == "주식일봉차트조회요청") code = "OPT10081";
            else if (sRQName == "주식주봉차트조회요청") code = "OPT10082";
            else if (sRQName == "주식월봉차트조회요청") code = "OPT10083";
            else if (sRQName == "당일전일체결요청") code = "OPT10084";
            else if (sRQName == "계좌수익률요청") code = "OPT10085";
            else if (sRQName == "일별주가요청") code = "OPT10086";
            else if (sRQName == "시간외단일가요청") code = "OPT10087";
            else if (sRQName == "주식년봉차트조회요청") code = "OPT10094";
            else if (sRQName == "업종현재가요청") code = "OPT20001";
            else if (sRQName == "업종별주가요청") code = "OPT20002";
            else if (sRQName == "전업종지수요청") code = "OPT20003";
            else if (sRQName == "업종틱차트조회요청") code = "OPT20004";
            else if (sRQName == "업종분봉조회요청") code = "OPT20005";
            else if (sRQName == "업종일봉조회요청") code = "OPT20006";
            else if (sRQName == "업종주봉조회요청") code = "OPT20007";
            else if (sRQName == "업종월봉조회요청") code = "OPT20008";
            else if (sRQName == "업종현재가일별요청") code = "OPT20009";
            else if (sRQName == "업종년봉조회요청") code = "OPT20019";
            else if (sRQName == "대차거래추이요청(종목별)") code = "OPT20068";
            else if (sRQName == "ELW가격급등락요청") code = "OPT30001";
            else if (sRQName == "거래원별ELW순매매상위요청") code = "OPT30002";
            else if (sRQName == "ELW LP보유일별추이요청") code = "OPT30003";
            else if (sRQName == "ELW괴리율요청") code = "OPT30004";
            else if (sRQName == "ELW조건검색요청") code = "OPT30005";
            else if (sRQName == "ELW종목상세요청") code = "OPT30007";
            else if (sRQName == "ELW민감도지표요청") code = "OPT30008";
            else if (sRQName == "ELW등락율순위요청") code = "OPT30009";
            else if (sRQName == "ELW잔량순위요청") code = "OPT30010";
            else if (sRQName == "ELW근접율요청") code = "OPT30011";
            else if (sRQName == "ELW종목상세정보요청") code = "OPT30012";
            else if (sRQName == "ETF수익율요청") code = "OPT40001";
            else if (sRQName == "ETF종목정보요청") code = "OPT40002";
            else if (sRQName == "ETF일별추이요청") code = "OPT40003";
            else if (sRQName == "ETF전체시세요청") code = "OPT40004";
            else if (sRQName == "ETF일별추이요청") code = "OPT40005";
            else if (sRQName == "ETF시간대별추이요청") code = "OPT40006";
            else if (sRQName == "ETF시간대별체결요청") code = "OPT40007";
            else if (sRQName == "ETF시간대별체결요청") code = "OPT40008";
            else if (sRQName == "ETF시간대별체결요청") code = "OPT40009";
            else if (sRQName == "ETF시간대별추이요청") code = "OPT40010";
            else if (sRQName == "선옵현재가정보요청") code = "OPT50001";
            else if (sRQName == "선옵일자별체결요청") code = "OPT50002";
            else if (sRQName == "선옵시고저가요청") code = "OPT50003";
            else if (sRQName == "콜옵션행사가요청") code = "OPT50004";
            else if (sRQName == "선옵시간별거래량요청") code = "OPT50005";
            else if (sRQName == "선옵체결추이요청") code = "OPT50006";
            else if (sRQName == "선물시세추이요청") code = "OPT50007";
            else if (sRQName == "프로그램매매추이차트요청") code = "OPT50008";
            else if (sRQName == "선옵시간별잔량요청") code = "OPT50009";
            else if (sRQName == "선옵호가잔량추이요청") code = "OPT50010";
            else if (sRQName == "선옵호가잔량추이요청") code = "OPT50011";
            else if (sRQName == "선옵타임스프레드차트요청") code = "OPT50012";
            else if (sRQName == "선물가격대별비중차트요청") code = "OPT50013";
            else if (sRQName == "선물가격대별비중차트요청") code = "OPT50014";
            else if (sRQName == "선물미결제약정일차트요청") code = "OPT50015";
            else if (sRQName == "베이시스추이차트요청") code = "OPT50016";
            else if (sRQName == "베이시스추이차트요청") code = "OPT50017";
            else if (sRQName == "풋콜옵션비율차트요청") code = "OPT50018";
            else if (sRQName == "선물옵션현재가정보요청") code = "OPT50019";
            else if (sRQName == "복수종목결제월별시세요청") code = "OPT50020";
            else if (sRQName == "콜종목결제월별시세요청") code = "OPT50021";
            else if (sRQName == "풋종목결제월별시세요청") code = "OPT50022";
            else if (sRQName == "민감도지표추이요청") code = "OPT50023";
            else if (sRQName == "일별변동성분석그래프요청") code = "OPT50024";
            else if (sRQName == "시간별변동성분석그래프요청") code = "OPT50025";
            else if (sRQName == "선옵주문체결요청") code = "OPT50026";
            else if (sRQName == "선옵잔고요청") code = "OPT50027";
            else if (sRQName == "선물틱차트요청") code = "OPT50028";
            else if (sRQName == "선물분차트요청") code = "OPT50029";
            else if (sRQName == "선물일차트요청") code = "OPT50030";
            else if (sRQName == "선옵잔고손익요청") code = "OPT50031";
            else if (sRQName == "선옵당일실현손익요청") code = "OPT50032";
            else if (sRQName == "선옵잔존일조회요청") code = "OPT50033";
            else if (sRQName == "선옵전일가격요청") code = "OPT50034";
            else if (sRQName == "지수변동성차트요청") code = "OPT50035";
            else if (sRQName == "주요지수변동성차트요청") code = "OPT50036";
            else if (sRQName == "코스피200지수요청") code = "OPT50037";
            else if (sRQName == "투자자별만기손익차트요청") code = "OPT50038";
            else if (sRQName == "선옵시고저가요청") code = "OPT50040";
            else if (sRQName == "주식선물거래량상위종목요청") code = "OPT50043";
            else if (sRQName == "주식선물시세표요청") code = "OPT50044";
            else if (sRQName == "선물미결제약정분차트요청") code = "OPT50062";
            else if (sRQName == "옵션미결제약정일차트요청") code = "OPT50063";
            else if (sRQName == "옵션미결제약정분차트요청") code = "OPT50064";
            else if (sRQName == "풋옵션행사가요청") code = "OPT50065";
            else if (sRQName == "옵션틱차트요청") code = "OPT50066";
            else if (sRQName == "옵션분차트요청") code = "OPT50067";
            else if (sRQName == "옵션일차트요청") code = "OPT50068";
            else if (sRQName == "선물주차트요청") code = "OPT50071";
            else if (sRQName == "선물월차트요청") code = "OPT50072";
            else if (sRQName == "선물년차트요청") code = "OPT50073";
            else if (sRQName == "테마그룹별요청") code = "OPT90001";
            else if (sRQName == "테마구성종목요청") code = "OPT90002";
            else if (sRQName == "프로그램순매수상위50요청") code = "OPT90003";
            else if (sRQName == "종목별프로그램매매현황요청") code = "OPT90004";
            else if (sRQName == "프로그램매매추이요청") code = "OPT90005";
            else if (sRQName == "프로그램매매차익잔고추이요청") code = "OPT90006";
            else if (sRQName == "프로그램매매누적추이요청") code = "OPT90007";
            else if (sRQName == "종목시간별프로그램매매추이요청") code = "OPT90008";
            else if (sRQName == "외국인기관매매상위요청") code = "OPT90009";
            else if (sRQName == "차익잔고현황요청") code = "OPT90010";
            else if (sRQName == "차익잔고현황요청") code = "OPT90011";
            else if (sRQName == "대차거래내역요청") code = "OPT90012";
            else if (sRQName == "종목일별프로그램매매추이요청") code = "OPT90013";
            else if (sRQName == "대차거래상위10종목요청") code = "OPT99999";
            else if (sRQName == "선물전체시세요청") code = "OPTFOFID";
            else if (sRQName == "관심종목정보요청") code = "OPTKWFID";
            else if (sRQName == "관심종목투자자정보요청") code = "OPTKWINV";
            else if (sRQName == "관심종목프로그램정보요청") code = "OPTKWPRO";
            else if (sRQName == "예수금상세현황요청") code = "OPW00001";
            else if (sRQName == "일별추정예탁자산현황요청") code = "OPW00002";
            else if (sRQName == "추정자산조회요청") code = "OPW00003";
            else if (sRQName == "계좌평가현황요청") code = "OPW00004";
            else if (sRQName == "체결잔고요청") code = "OPW00005";
            else if (sRQName == "관리자별주문체결내역요청") code = "OPW00006";
            else if (sRQName == "계좌별주문체결내역상세요청") code = "OPW00007";
            else if (sRQName == "계좌별익일결제예정내역요청") code = "OPW00008";
            else if (sRQName == "계좌별주문체결현황요청") code = "OPW00009";
            else if (sRQName == "주문인출가능금액요청") code = "OPW00010";
            else if (sRQName == "증거금율별주문가능수량조회요청") code = "OPW00011";
            else if (sRQName == "신용보증금율별주문가능수량조회요청") code = "OPW00012";
            else if (sRQName == "증거금세부내역조회요청") code = "OPW00013";
            else if (sRQName == "비밀번호일치여부요청") code = "OPW00014";
            else if (sRQName == "위탁종합거래내역요청") code = "OPW00015";
            else if (sRQName == "일별계좌수익률상세현황요청") code = "OPW00016";
            else if (sRQName == "계좌별당일현황요청") code = "OPW00017";
            else if (sRQName == "계좌평가잔고내역요청") code = "OPW00018";
            else if (sRQName == "ELW종목별민감도지표요청") code = "OPW10001";
            else if (sRQName == "ELW투자지표요청") code = "OPW10002";
            else if (sRQName == "ELW민감도지표요청") code = "OPW10003";
            else if (sRQName == "업종별순매수요청") code = "OPW10004";
            else if (sRQName == "선물옵션청산주문위탁증거금가계산요청") code = "OPW20001";
            else if (sRQName == "선옵당일매매변동현황요청") code = "OPW20002";
            else if (sRQName == "선옵기간손익조회요청") code = "OPW20003";
            else if (sRQName == "선옵주문체결내역상세요청") code = "OPW20004";
            else if (sRQName == "선옵주문체결내역상세평균가요청") code = "OPW20005";
            else if (sRQName == "선옵잔고상세현황요청") code = "OPW20006";
            else if (sRQName == "선옵잔고현황정산가기준요청") code = "OPW20007";
            else if (sRQName == "계좌별결제예상내역조회요청") code = "OPW20008";
            else if (sRQName == "선옵계좌별주문가능수량요청") code = "OPW20009";
            else if (sRQName == "선옵예탁금및증거금조회요청") code = "OPW20010";
            else if (sRQName == "선옵계좌예비증거금상세요청") code = "OPW20011";
            else if (sRQName == "선옵증거금상세내역요청") code = "OPW20012";
            else if (sRQName == "계좌미결제청산가능수량조회요청") code = "OPW20013";
            else if (sRQName == "선옵실시간증거금산출요청") code = "OPW20014";
            else if (sRQName == "옵션매도주문증거금현황요청") code = "OPW20015";
            else if (sRQName == "신용융자 가능종목요청") code = "OPW20016";
            else if (sRQName == "신용융자 가능문의") code = "OPW20017";

            return code;
        }

        #endregion
        
        #region TR Functions
        ///<summary> 코드명:OPT10001 기능명:주식기본정보요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        public void GetOPT10001(string callbackID, string arg1)
        {
            Console.WriteLine("OPT0001" + arg1);
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            CommRqData("주식기본정보요청", "opt10001", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT10002 기능명:주식거래원요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        public void GetOPT10002(string callbackID, string arg1)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            CommRqData("주식거래원요청", "OPT10002", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT10003 기능명:체결정보요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        public void GetOPT10003(string callbackID, string arg1)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            CommRqData("체결정보요청", "OPT10003", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT10004 기능명:주식호가요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        public void GetOPT10004(string callbackID, string arg1)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            CommRqData("주식호가요청", "OPT10004", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT10005 기능명:주식일주월시분요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        public void GetOPT10005(string callbackID, string arg1)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            
            CommRqData("주식일주월시분요청", "OPT10005", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT10006 기능명:주식시분요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        public void GetOPT10006(string callbackID, string arg1)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            CommRqData("주식시분요청", "OPT10006", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT10007 기능명:시세표성정보요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        public void GetOPT10007(string callbackID, string arg1)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            CommRqData("시세표성정보요청", "OPT10007", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT10008 기능명:주식외국인요청</summary>
        ///<param name="arg2">종목코드 : 000:전체, 001:코스피, 101:코스닥</param>
        ///<param name="arg3">시작일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        public void GetOPT10008(string callbackID, string arg2, string arg3)
        {
            nowCallbackID = callbackID;
            string screenCode = "0241";
            SetInputValue("화면번호", screenCode);
            SetInputValue("종목코드", arg2);
            SetInputValue("시작일자", arg3);
            CommRqData("주식외국인요청", "OPT10008", 0, screenCode);
        }

        ///<summary> 코드명:OPT10009 기능명:주식기관요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        public void GetOPT10009(string callbackID, string arg1)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            CommRqData("주식기관요청", "OPT10009", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT10010 기능명:업종프로그램요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        public void GetOPT10010(string callbackID, string arg1)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            CommRqData("업종프로그램요청", "OPT10010", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT10012 기능명:주문체결요청</summary>
        ///<param name="arg1">계좌번호 : 전문 조회할 보유계좌번호</param>
        public void GetOPT10012(string callbackID, string arg1)
        {
            nowCallbackID = callbackID;
            SetInputValue("계좌번호", arg1);
            CommRqData("주문체결요청", "OPT10012", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT10013 기능명:신용매매동향요청</summary>
        ///<param name="arg2">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg3">일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg4">조회구분 : 1:융자, 2:대주</param>
        public void GetOPT10013(string callbackID, string arg2, string arg3, string arg4)
        {
            nowCallbackID = callbackID;
            string screenCode = "0141";
            SetInputValue("화면번호", screenCode);
            SetInputValue("종목코드", arg2);
            SetInputValue("일자", arg3);
            SetInputValue("조회구분", arg4);
            logger.Info("GetOPT10013");
            CommRqData("신용매매동향요청", "OPT10013", 0, screenCode);
        }

        ///<summary> 코드명:OPT10014 기능명:공매도추이요청</summary>
        ///<param name="arg2">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg3">시간구분 : 0:시작일, 1:기간</param>
        ///<param name="arg4">시작일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg5">종료일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        public void GetOPT10014(string callbackID, string arg2, string arg3, string arg4, string arg5)
        {
            nowCallbackID = callbackID;
            string screenCode = "0142";
            SetInputValue("화면번호", screenCode);
            SetInputValue("종목코드", arg2);
            SetInputValue("시간구분", arg3);
            SetInputValue("시작일자", arg4);
            SetInputValue("종료일자", arg5);
            CommRqData("공매도추이요청", "OPT10014", 0, screenCode);
        }

        ///<summary> 코드명:OPT10015 기능명:일별거래상세요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg2">시작일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        public void GetOPT10015(string callbackID, string arg1, string arg2)
        {
            nowCallbackID = callbackID;
            string screenCode = "0143";
            
            SetInputValue("화면번호", screenCode);
            SetInputValue("종목코드", arg1); 
            SetInputValue("시작일자", arg2);
            CommRqData("일별거래상세요청", "OPT10015", 0, screenCode);
        }

        ///<summary> 코드명:OPT10016 기능명:신고저가요청</summary>
        ///<param name="arg2">시장구분 : 000:전체, 001:코스피, 101:코스닥</param>
        ///<param name="arg3">신고저구분 : 1:신고가,2:신저가</param>
        ///<param name="arg4">고저종구분 : 1:고저기준, 2:종가기준</param>
        ///<param name="arg5">종목조건 : 0:전체조회,1:관리종목제외, 3:우선주제외, 5:증100제외, 6:증100만보기, 7:증40만보기, 8:증30만보기</param>
        ///<param name="arg6">거래량구분 : 00000:전체조회, 00010:만주이상, 00050:5만주이상, 00100:10만주이상, 00150:15만주이상, 00200:20만주이상, 00300:30만주이상, 00500:50만주이상, 01000:백만주이상</param>
        ///<param name="arg7">신용조건 : 0:전체조회, 1:신용융자A군, 2:신용융자B군, 3:신용융자C군, 4:신용융자D군, 9:신용융자전체</param>
        ///<param name="arg8">상하한포함 : 0:미포함, 1:포함</param>
        ///<param name="arg9">기간 : 5:5일, 10:10일, 20:20일, 60:60일, 250:250일, 250일까지 입력가능</param>
        public void GetOPT10016(string callbackID, string arg2, string arg3, string arg4, string arg5, string arg6, string arg7, string arg8, string arg9)
        {
            nowCallbackID = callbackID;
            string screenCode = "0161";
            SetInputValue("화면번호", screenCode);
            SetInputValue("시장구분", arg2);
            SetInputValue("신고저구분", arg3);
            SetInputValue("고저종구분", arg4);
            SetInputValue("종목조건", arg5);
            SetInputValue("거래량구분", arg6);
            SetInputValue("신용조건", arg7);
            SetInputValue("상하한포함", arg8);
            SetInputValue("기간", arg9);
            CommRqData("신고저가요청", "OPT10016", 0, screenCode);
        }

        ///<summary> 코드명:OPT10017 기능명:상하한가요청</summary>
        ///<param name="arg2">시장구분 : 000:전체, 001:코스피, 101:코스닥</param>
        ///<param name="arg3">상하한구분 : 1:상한, 2:상승, 3:보합, 4: 하한, 5:하락, 6:전일상한, 7:전일하한</param>
        ///<param name="arg4">정렬구분 : 1:종목코드순, 2:연속횟수순(상위100개), 3:등락률순</param>
        ///<param name="arg5">종목조건 : 0:전체조회,1:관리종목제외, 3:우선주제외, 4:우선주+관리종목제외, 5:증100제외, 6:증100만 보기, 7:증40만 보기, 8:증30만 보기, 9:증20만 보기, 10:우선주+관리종목+환기종목제외</param>
        ///<param name="arg6">거래량구분 : 00000:전체조회, 00010:만주이상, 00050:5만주이상, 00100:10만주이상, 00150:15만주이상, 00200:20만주이상, 00300:30만주이상, 00500:50만주이상, 01000:백만주이상</param>
        ///<param name="arg7">신용조건 : 0:전체조회, 1:신용융자A군, 2:신용융자B군, 3:신용융자C군, 4:신용융자D군, 9:신용융자전체</param>
        ///<param name="arg8">매매금구분 : 0:전체조회, 1:1천원미만, 2:1천원~2천원, 3:2천원~3천원, 4:5천원~1만원, 5:1만원이상, 8:1천원이상</param>
        public void GetOPT10017(string callbackID, string arg2, string arg3, string arg4, string arg5, string arg6, string arg7, string arg8)
        {
            nowCallbackID = callbackID;
            string screenCode = "0162";
            SetInputValue("화면번호", screenCode);
            SetInputValue("시장구분", arg2);
            SetInputValue("상하한구분", arg3);
            SetInputValue("정렬구분", arg4);
            SetInputValue("종목조건", arg5);
            SetInputValue("거래량구분", arg6);
            SetInputValue("신용조건", arg7);
            SetInputValue("매매금구분", arg8);
            CommRqData("상하한가요청", "OPT10017", 0, screenCode);
        }

        ///<summary> 코드명:OPT10018 기능명:고저가근접요청</summary>
        ///<param name="arg2">고저구분 : 1:고가, 2:저가</param>
        ///<param name="arg3">근접율 : 05:0.5 10:1.0, 15:1.5, 20:2.0. 25:2.5, 30:3.0 </param>
        ///<param name="arg4">시장구분 : 000:전체, 001:코스피, 101:코스닥</param>
        ///<param name="arg5">거래량구분 : 00000:전체조회, 00010:만주이상, 00050:5만주이상, 00100:10만주이상, 00150:15만주이상, 00200:20만주이상, 00300:30만주이상, 00500:50만주이상, 01000:백만주이상</param>
        ///<param name="arg6">종목조건 : 0:전체조회,1:관리종목제외, 3:우선주제외, 5:증100제외, 6:증100만보기, 7:증40만보기, 8:증30만보기</param>
        ///<param name="arg7">신용조건 : 0:전체조회, 1:신용융자A군, 2:신용융자B군, 3:신용융자C군, 4:신용융자D군, 9:신용융자전체</param>
        public void GetOPT10018(string callbackID, string arg2, string arg3, string arg4, string arg5, string arg6, string arg7)
        {
            nowCallbackID = callbackID;
            string screenCode = "0163";
            SetInputValue("화면번호", screenCode);
            SetInputValue("고저구분", arg2);
            SetInputValue("근접율", arg3);
            SetInputValue("시장구분", arg4);
            SetInputValue("거래량구분", arg5);
            SetInputValue("종목조건", arg6);
            SetInputValue("신용조건", arg7);
            CommRqData("고저가근접요청", "OPT10018", 0, screenCode);
        }

        ///<summary> 코드명:OPT10019 기능명:가격급등락요청</summary>
        ///<param name="arg2">시장구분 : 000:전체, 001:코스피, 101:코스닥, 201:코스피200</param>
        ///<param name="arg3">등락구분 : 1:급등, 2:급락</param>
        ///<param name="arg4">시간구분 : 1:분전, 2:일전</param>
        ///<param name="arg5">시간 : 분 혹은 일입력</param>
        ///<param name="arg6">거래량구분 : 00000:전체조회, 00010:만주이상, 00050:5만주이상, 00100:10만주이상, 00150:15만주이상, 00200:20만주이상, 00300:30만주이상, 00500:50만주이상, 01000:백만주이상</param>
        ///<param name="arg7">종목조건 : 0:전체조회,1:관리종목제외, 3:우선주제외, 5:증100제외, 6:증100만보기, 7:증40만보기, 8:증30만보기</param>
        ///<param name="arg8">신용조건 : 0:전체조회, 1:신용융자A군, 2:신용융자B군, 3:신용융자C군, 4:신용융자D군, 9:신용융자전체</param>
        ///<param name="arg9">가격조건 : 0:전체조회, 1:1천원미만, 2:1천원~2천원, 3:2천원~3천원, 4:5천원~1만원, 5:1만원이상, 8:1천원이상</param>
        ///<param name="arg10">상하한포함 : 0:미포함, 1:포함</param>
        public void GetOPT10019(string callbackID, string arg2, string arg3, string arg4, string arg5, string arg6, string arg7, string arg8, string arg9, string arg10)
        {
            nowCallbackID = callbackID;
            string screenCode = "0164";
            SetInputValue("화면번호", screenCode);
            SetInputValue("시장구분", arg2);
            SetInputValue("등락구분", arg3);
            SetInputValue("시간구분", arg4);
            SetInputValue("시간", arg5);
            SetInputValue("거래량구분", arg6);
            SetInputValue("종목조건", arg7);
            SetInputValue("신용조건", arg8);
            SetInputValue("가격조건", arg9);
            SetInputValue("상하한포함", arg10);
            CommRqData("가격급등락요청", "OPT10019", 0, screenCode);
        }

        ///<summary> 코드명:OPT10020 기능명:호가잔량상위요청</summary>
        ///<param name="arg2">시장구분 : 001:코스피, 101:코스닥</param>
        ///<param name="arg3">정렬구분 : 1:순매수잔량순, 2:순매도잔량순, 3:매수비율순, 4:매도비율순</param>
        ///<param name="arg4">거래량구분 : 0000:장시작전(0주이상), 0010:만주이상, 0050:5만주이상, 00100:10만주이상</param>
        ///<param name="arg5">종목조건 : 0:전체조회, 1:관리종목제외, 5:증100제외, 6:증100만보기, 7:증40만보기, 8:증30만보기, 9:증20만보기</param>
        ///<param name="arg6">신용조건 : 0:전체조회, 1:신용융자A군, 2:신용융자B군, 3:신용융자C군, 4:신용융자D군, 9:신용융자전체</param>
        public void GetOPT10020(string callbackID, string arg2, string arg3, string arg4, string arg5, string arg6)
        {
            nowCallbackID = callbackID;
            string screenCode = "0165";
            SetInputValue("화면번호", screenCode);
            SetInputValue("시장구분", arg2);
            SetInputValue("정렬구분", arg3);
            SetInputValue("거래량구분", arg4);
            SetInputValue("종목조건", arg5);
            SetInputValue("신용조건", arg6);
            CommRqData("호가잔량상위요청", "OPT10020", 0, screenCode);
        }

        ///<summary> 코드명:OPT10021 기능명:호가잔량급증요청</summary>
        ///<param name="arg2">시장구분 : 001:코스피, 101:코스닥</param>
        ///<param name="arg3">매매구분 : 1:매수잔량, 2:매도잔량</param>
        ///<param name="arg4">정렬구분 : 1:급증량, 2:급증률</param>
        ///<param name="arg5">시간구분 : 분 입력</param>
        ///<param name="arg6">거래량구분 : 1:천주이상, 5:5천주이상, 10:만주이상, 50:5만주이상, 100:10만주이상</param>
        ///<param name="arg7">종목조건 : 0:전체조회, 1:관리종목제외, 5:증100제외, 6:증100만보기, 7:증40만보기, 8:증30만보기, 9:증20만보기</param>
        public void GetOPT10021(string callbackID, string arg2, string arg3, string arg4, string arg5, string arg6, string arg7)
        {
            nowCallbackID = callbackID;
            string screenCode = "0166";
            SetInputValue("화면번호", screenCode);
            SetInputValue("시장구분", arg2);
            SetInputValue("매매구분", arg3);
            SetInputValue("정렬구분", arg4);
            SetInputValue("시간구분", arg5);
            SetInputValue("거래량구분", arg6);
            SetInputValue("종목조건", arg7);
            CommRqData("호가잔량급증요청", "OPT10021", 0, screenCode);
        }

        ///<summary> 코드명:OPT10022 기능명:잔량율급증요청</summary>
        ///<param name="arg2">시장구분 : 001:코스피, 101:코스닥</param>
        ///<param name="arg3">비율구분 : 1:매수/매도비율, 2:매도/매수비율</param>
        ///<param name="arg4">시간구분 : 분 입력</param>
        ///<param name="arg5">거래량구분 : 5:5천주이상, 10:만주이상, 50:5만주이상, 100:10만주이상</param>
        ///<param name="arg6">종목조건 : 0:전체조회, 1:관리종목제외, 5:증100제외, 6:증100만보기, 7:증40만보기, 8:증30만보기, 9:증20만보기</param>
        public void GetOPT10022(string callbackID, string arg2, string arg3, string arg4, string arg5, string arg6)
        {
            nowCallbackID = callbackID;
            string screenCode = "0167";
            SetInputValue("화면번호", screenCode);
            SetInputValue("시장구분", arg2);
            SetInputValue("비율구분", arg3);
            SetInputValue("시간구분", arg4);
            SetInputValue("거래량구분", arg5);
            SetInputValue("종목조건", arg6);
            CommRqData("잔량율급증요청", "OPT10022", 0, screenCode);
        }

        ///<summary> 코드명:OPT10023 기능명:거래량급증요청</summary>
        ///<param name="arg2">시장구분 : 000:전체, 001:코스피, 101:코스닥</param>
        ///<param name="arg3">정렬구분 : 1:급증량, 2:급증률</param>
        ///<param name="arg4">시간구분 : 1:분, 2:전일</param>
        ///<param name="arg5">거래량구분 : 5:5천주이상, 10:만주이상, 50:5만주이상, 100:10만주이상, 200:20만주이상, 300:30만주이상, 500:50만주이상, 1000:백만주이상</param>
        ///<param name="arg6">시간 : 분 입력</param>
        ///<param name="arg7">종목조건 : 0:전체조회, 1:관리종목제외, 5:증100제외, 6:증100만보기, 7:증40만보기, 8:증30만보기, 9:증20만보기</param>
        ///<param name="arg8">가격구분 : 0:전체조회, 2:5만원이상, 5:1만원이상, 6:5천원이상, 8:1천원이상, 9:10만원이상</param>
        public void GetOPT10023(string callbackID, string arg2, string arg3, string arg4, string arg5, string arg6, string arg7, string arg8)
        {
            nowCallbackID = callbackID;
            string screenCode = "0168";
            SetInputValue("화면번호", screenCode);
            SetInputValue("시장구분", arg2);
            SetInputValue("정렬구분", arg3);
            SetInputValue("시간구분", arg4);
            SetInputValue("거래량구분", arg5);
            SetInputValue("시간", arg6);
            SetInputValue("종목조건", arg7);
            SetInputValue("가격구분", arg8);
            CommRqData("거래량급증요청", "OPT10023", 0, screenCode);
        }

        ///<summary> 코드명:OPT10024 기능명:거래량갱신요청</summary>
        ///<param name="arg2">시장구분 : 000:전체, 001:코스피, 101:코스닥</param>
        ///<param name="arg3">주기구분 : 5:5일, 10:10일, 20:20일, 60:60일, 250:250일</param>
        ///<param name="arg4">거래량구분 : 5:5천주이상, 10:만주이상, 50:5만주이상, 100:10만주이상, 200:20만주이상, 300:30만주이상, 500:50만주이상, 1000:백만주이상</param>
        public void GetOPT10024(string callbackID, string arg2, string arg3, string arg4)
        {
            nowCallbackID = callbackID;
            string screenCode = "0169";
            SetInputValue("화면번호", screenCode);
            SetInputValue("시장구분", arg2);
            SetInputValue("주기구분", arg3);
            SetInputValue("거래량구분", arg4);
            CommRqData("거래량갱신요청", "OPT10024", 0, screenCode);
        }

        ///<summary> 코드명:OPT10025 기능명:매물대집중요청</summary>
        ///<param name="arg2">시장구분 : 000:전체, 001:코스피, 101:코스닥</param>
        ///<param name="arg3">매물집중비율 : 0~100 입력</param>
        ///<param name="arg4">현재가진입 : 0:현재가 매물대 집입 포함안함, 1:현재가 매물대 집입포함</param>
        ///<param name="arg5">매물대수 : 숫자입력</param>
        ///<param name="arg6">주기구분 : 50:50일, 100:100일, 150:150일, 200:200일, 250:250일, 300:300일</param>
        public void GetOPT10025(string callbackID, string arg2, string arg3, string arg4, string arg5, string arg6)
        {
            nowCallbackID = callbackID;
            string screenCode = "0170";
            SetInputValue("화면번호", screenCode);
            SetInputValue("시장구분", arg2);
            SetInputValue("매물집중비율", arg3);
            SetInputValue("현재가진입", arg4);
            SetInputValue("매물대수", arg5);
            SetInputValue("주기구분", arg6);
            CommRqData("매물대집중요청", "OPT10025", 0, screenCode);
        }

        ///<summary> 코드명:OPT10026 기능명:고저PER요청</summary>
        ///<param name="arg2">PER구분 : 1:코스피저PER, 2:코스피고PER, 3:코스닥저PER, 4:코스닥고PER</param>
        public void GetOPT10026(string callbackID, string arg2)
        {
            nowCallbackID = callbackID;
            string screenCode = "0171";
            SetInputValue("화면번호", screenCode);
            SetInputValue("PER구분", arg2);
            CommRqData("고저PER요청", "OPT10026", 0, screenCode);
        }

        ///<summary> 코드명:OPT10027 기능명:전일대비등락률상위요청</summary>
        ///<param name="arg2">시장구분 : 000:전체, 001:코스피, 101:코스닥</param>
        ///<param name="arg3">정렬구분 : 1:상승률, 2:상승폭, 3:하락률, 4:하락폭</param>
        ///<param name="arg4">거래량조건 : 0000:전체조회, 0010:만주이상, 0050:5만주이상, 0100:10만주이상, 0150:15만주이상, 0200:20만주이상, 0300:30만주이상, 0500:50만주이상, 1000:백만주이상</param>
        ///<param name="arg5">종목조건 : 0:전체조회, 1:관리종목제외, 4:우선주+관리주제외, 3:우선주제외, 5:증100제외, 6:증100만보기, 7:증40만보기, 8:증30만보기, 9:증20만보기, 11:정리매매종목제외</param>
        ///<param name="arg6">신용조건 : 0:전체조회, 1:신용융자A군, 2:신용융자B군, 3:신용융자C군, 4:신용융자D군, 9:신용융자전체</param>
        ///<param name="arg7">상하한포함 : 0:불 포함, 1:포함</param>
        ///<param name="arg8">가격조건 : 0:전체조회, 1:1천원미만, 2:1천원~2천원, 3:2천원~5천원, 4:5천원~1만원, 5:1만원이상, 8:1천원이상</param>
        ///<param name="arg9">거래대금조건 : 0:전체조회, 3:3천만원이상, 5:5천만원이상, 10:1억원이상, 30:3억원이상, 50:5억원이상, 100:10억원이상, 300:30억원이상, 500:50억원이상, 1000:100억원이상, 3000:300억원이상, 5000:500억원이상</param>
        public void GetOPT10027(string callbackID, string arg2, string arg3, string arg4, string arg5, string arg6, string arg7, string arg8, string arg9)
        {
            nowCallbackID = callbackID;
            string screenCode = "0181";
            SetInputValue("화면번호", screenCode);
            SetInputValue("시장구분", arg2);
            SetInputValue("정렬구분", arg3);
            SetInputValue("거래량조건", arg4);
            SetInputValue("종목조건", arg5);
            SetInputValue("신용조건", arg6);
            SetInputValue("상하한포함", arg7);
            SetInputValue("가격조건", arg8);
            SetInputValue("거래대금조건", arg9);
            CommRqData("전일대비등락률상위요청", "OPT10027", 0, screenCode);
        }

        ///<summary> 코드명:OPT10028 기능명:시가대비등락률요청</summary>
        ///<param name="arg2">정렬구분 : 1:시가, 2:고가, 3:저가, 4:기준가</param>
        ///<param name="arg3">거래량조건 : 0000:전체조회, 0010:만주이상, 0050:5만주이상, 0100:10만주이상, 0500:50만주이상, 1000:백만주이상</param>
        ///<param name="arg4">시장구분 : 000:전체, 001:코스피, 101:코스닥</param>
        ///<param name="arg5">상하한포함 : 0:불 포함, 1:포함</param>
        ///<param name="arg6">종목조건 : 0:전체조회, 1:관리종목제외, 4:우선주+관리주제외, 3:우선주제외, 5:증100제외, 6:증100만보기, 7:증40만보기, 8:증30만보기, 9:증20만보기</param>
        ///<param name="arg7">신용조건 : 0:전체조회, 1:신용융자A군, 2:신용융자B군, 3:신용융자C군, 4:신용융자D군, 9:신용융자전체</param>
        ///<param name="arg8">거래대금조건 : 0:전체조회, 3:3천만원이상, 5:5천만원이상, 10:1억원이상, 30:3억원이상, 50:5억원이상, 100:10억원이상, 300:30억원이상, 500:50억원이상, 1000:100억원이상, 3000:300억원이상, 5000:500억원이상</param>
        ///<param name="arg9">등락조건 : 1:상위, 2:하위</param>
        public void GetOPT10028(string callbackID, string arg2, string arg3, string arg4, string arg5, string arg6, string arg7, string arg8, string arg9)
        {
            nowCallbackID = callbackID;
            string screenCode = "0182";
            SetInputValue("화면번호", screenCode);
            SetInputValue("정렬구분", arg2);
            SetInputValue("거래량조건", arg3);
            SetInputValue("시장구분", arg4);
            SetInputValue("상하한포함", arg5);
            SetInputValue("종목조건", arg6);
            SetInputValue("신용조건", arg7);
            SetInputValue("거래대금조건", arg8);
            SetInputValue("등락조건", arg9);
            CommRqData("시가대비등락률요청", "OPT10028", 0, screenCode);
        }

        ///<summary> 코드명:OPT10029 기능명:예상체결등락률상위요청</summary>
        ///<param name="arg2">시장구분 : 000:전체, 001:코스피, 101:코스닥</param>
        ///<param name="arg3">정렬구분 : 1:상승률, 2:상승폭, 3:보합, 4:하락률,5:하락폭, 6, 체결량, 7:상한, 8:하한</param>
        ///<param name="arg4">거래량조건 : 0:전체조회, 1;천주이상, 3:3천주, 5:5천주, 10:만주이상, 50:5만주이상, 100:10만주이상</param>
        ///<param name="arg5">종목조건 : 0:전체조회, 1:관리종목제외, 3:우선주제외, 5:증100제외, 6:증100만보기, 7:증40만보기, 8:증30만보기, 9:증20만보기, 11:정리매매종목제외</param>
        ///<param name="arg6">신용조건 : 0:전체조회, 1:신용융자A군, 2:신용융자B군, 3:신용융자C군, 4:신용융자D군, 9:신용융자전체</param>
        ///<param name="arg7">가격조건 : 0:전체조회, 1:1천원미만, 2:1천원~2천원, 3:2천원~5천원, 4:5천원~1만원, 5:1만원이상, 8:1천원이상</param>
        public void GetOPT10029(string callbackID, string arg2, string arg3, string arg4, string arg5, string arg6, string arg7)
        {
            nowCallbackID = callbackID;
            string screenCode = "0183";
            SetInputValue("화면번호", screenCode);
            SetInputValue("시장구분", arg2);
            SetInputValue("정렬구분", arg3);
            SetInputValue("거래량조건", arg4);
            SetInputValue("종목조건", arg5);
            SetInputValue("신용조건", arg6);
            SetInputValue("가격조건", arg7);
            CommRqData("예상체결등락률상위요청", "OPT10029", 0, screenCode);
        }

        ///<summary> 코드명:OPT10030 기능명:당일거래량상위요청</summary>
        ///<param name="arg2">시장구분 : 000:전체, 001:코스피, 101:코스닥</param>
        ///<param name="arg3">정렬구분 : 1:거래량, 2:거래회전율, 3:거래대금</param>
        ///<param name="arg4">관리종목포함 : 0:관리종목 포함, 1:관리종목 미포함</param>
        public void GetOPT10030(string callbackID, string arg2, string arg3, string arg4)
        {
            nowCallbackID = callbackID;
            string screenCode = "0184";
            SetInputValue("화면번호", screenCode);
            SetInputValue("시장구분", arg2);
            SetInputValue("정렬구분", arg3);
            SetInputValue("관리종목포함", arg4);
            CommRqData("당일거래량상위요청", "OPT10030", 0, screenCode);
        }

        ///<summary> 코드명:OPT10031 기능명:전일거래량상위요청</summary>
        ///<param name="arg2">시장구분 : 000:전체, 001:코스피, 101:코스닥</param>
        ///<param name="arg3">조회구분 : 1:전일거래량 상위100종목, 2:전일거래대금 상위100종목</param>
        ///<param name="arg4">순위시작 : 0 ~ 100 값 중에  조회를 원하는 순위 시작값</param>
        ///<param name="arg5">순위끝 : 0 ~ 100 값 중에  조회를 원하는 순위 끝값</param>
        public void GetOPT10031(string callbackID, string arg2, string arg3, string arg4, string arg5)
        {
            nowCallbackID = callbackID;
            string screenCode = "0185";
            SetInputValue("화면번호", screenCode);
            SetInputValue("시장구분", arg2);
            SetInputValue("조회구분", arg3);
            SetInputValue("순위시작", arg4);
            SetInputValue("순위끝", arg5);
            CommRqData("전일거래량상위요청", "OPT10031", 0, screenCode);
        }

        ///<summary> 코드명:OPT10032 기능명:거래대금상위요청</summary>
        ///<param name="arg2">시장구분 : 000:전체, 001:코스피, 101:코스닥</param>
        ///<param name="arg3">관리종목포함 : 0:관리종목 미포함, 1:관리종목 포함</param>
        public void GetOPT10032(string callbackID, string arg2, string arg3)
        {
            nowCallbackID = callbackID;
            string screenCode = "1086";
            SetInputValue("화면번호", screenCode);
            SetInputValue("시장구분", arg2);
            SetInputValue("관리종목포함", arg3);
            CommRqData("거래대금상위요청", "OPT10032", 0, screenCode);
        }

        ///<summary> 코드명:OPT10033 기능명:신용비율상위요청</summary>
        ///<param name="arg2">시장구분 : 000:전체, 001:코스피, 101:코스닥</param>
        ///<param name="arg3">거래량구분 : 0:전체조회, 10:만주이상, 50:5만주이상, 100:10만주이상, 200:20만주이상, 300:30만주이상, 500:50만주이상, 1000:백만주이상</param>
        ///<param name="arg4">종목조건 : 0:전체조회, 1:관리종목제외, 5:증100제외, 6:증100만보기, 7:증40만보기, 8:증30만보기, 9:증20만보기</param>
        ///<param name="arg5">상하한포함 : 0:상하한 미포함, 1:상하한포함</param>
        ///<param name="arg6">신용조건 : 0:전체조회, 1:신용융자A군, 2:신용융자B군, 3:신용융자C군, 4:신용융자D군, 9:신용융자전체</param>
        public void GetOPT10033(string callbackID, string arg2, string arg3, string arg4, string arg5, string arg6)
        {
            nowCallbackID = callbackID;
            string screenCode = "0188";
            SetInputValue("화면번호", screenCode);
            SetInputValue("시장구분", arg2);
            SetInputValue("거래량구분", arg3);
            SetInputValue("종목조건", arg4);
            SetInputValue("상하한포함", arg5);
            SetInputValue("신용조건", arg6);
            CommRqData("신용비율상위요청", "OPT10033", 0, screenCode);
        }

        ///<summary> 코드명:OPT10034 기능명:외인기간별매매상위요청</summary>
        ///<param name="arg2">시장구분 : 000:전체, 001:코스피, 101:코스닥</param>
        ///<param name="arg3">매매구분 : 1:순매도, 2:순매수, 3:순매매</param>
        ///<param name="arg4">기간 : 0:당일, 1:전일, 5:5일, 10;10일, 20:20일, 60:60일 </param>
        public void GetOPT10034(string callbackID, string arg2, string arg3, string arg4)
        {
            nowCallbackID = callbackID;
            string screenCode = "0242";
            SetInputValue("화면번호", screenCode);
            SetInputValue("시장구분", arg2);
            SetInputValue("매매구분", arg3);
            SetInputValue("기간", arg4);
            CommRqData("외인기간별매매상위요청", "OPT10034", 0, screenCode);
        }

        ///<summary> 코드명:OPT10035 기능명:외인연속순매매상위요청</summary>
        ///<param name="arg2">시장구분 : 000:전체, 001:코스피, 101:코스닥</param>
        ///<param name="arg3">매매구분 : 1:연속순매도, 2:연속순매수</param>
        ///<param name="arg4">기준일구분 : 0:당일기준, 1:전일기준</param>
        public void GetOPT10035(string callbackID, string arg2, string arg3, string arg4)
        {
            nowCallbackID = callbackID;
            string screenCode = "0243";
            SetInputValue("화면번호", screenCode);
            SetInputValue("시장구분", arg2);
            SetInputValue("매매구분", arg3);
            SetInputValue("기준일구분", arg4);
            CommRqData("외인연속순매매상위요청", "OPT10035", 0, screenCode);
        }

        ///<summary> 코드명:OPT10036 기능명:매매상위요청</summary>
        ///<param name="arg2">시장구분 : 000:전체, 001:코스피, 101:코스닥</param>
        ///<param name="arg3">기간 : 0:당일, 1:전일, 5:5일, 10;10일, 20:20일, 60:60일 </param>
        public void GetOPT10036(string callbackID, string arg2, string arg3)
        {
            nowCallbackID = callbackID;
            string screenCode = "0244";
            SetInputValue("화면번호", screenCode);
            SetInputValue("시장구분", arg2);
            SetInputValue("기간", arg3);
            CommRqData("매매상위요청", "OPT10036", 0, screenCode);
        }

        ///<summary> 코드명:OPT10037 기능명:외국계창구매매상위요청</summary>
        ///<param name="arg2">시장구분 : 000:전체, 001:코스피, 101:코스닥</param>
        ///<param name="arg3">기간 : 0:당일, 1:전일, 5:5일, 10;10일, 20:20일, 60:60일 </param>
        ///<param name="arg4">매매구분 : 1:순매수, 2:순매도, 3:매수, 4:매도</param>
        ///<param name="arg5">정렬구분 : 1:금액, 2:수량</param>
        ///<param name="arg6">현재가조건 : </param>
        ///<param name="arg7">가격조건 : 0:전체조회, 10000:1천원이상, 3000:3천원이상, 5000:5천원이상, 10000:1만원이상, 50000:5만원이상, 100000:10만원이상</param>
        public void GetOPT10037(string callbackID, string arg2, string arg3, string arg4, string arg5, string arg6, string arg7)
        {
            nowCallbackID = callbackID;
            string screenCode = "0246";
            SetInputValue("화면번호", screenCode);
            SetInputValue("시장구분", arg2);
            SetInputValue("기간", arg3);
            SetInputValue("매매구분", arg4);
            SetInputValue("정렬구분", arg5);
            SetInputValue("현재가조건", arg6);
            SetInputValue("가격조건", arg7);
            CommRqData("외국계창구매매상위요청", "OPT10037", 0, screenCode);
        }

        ///<summary> 코드명:OPT10038 기능명:종목별증권사순위요청</summary>
        ///<param name="arg2">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg3">시작일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg4">종료일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg5">조회구분 : 1:순매도순위정렬, 2:순매수순위정렬</param>
        ///<param name="arg6">기간 : </param>
        public void GetOPT10038(string callbackID, string arg2, string arg3, string arg4, string arg5, string arg6)
        {
            nowCallbackID = callbackID;
            string screenCode = "0251";
            SetInputValue("화면번호", screenCode);
            SetInputValue("종목코드", arg2);
            SetInputValue("시작일자", arg3);
            SetInputValue("종료일자", arg4);
            SetInputValue("조회구분", arg5);
            SetInputValue("기간", arg6);
            CommRqData("종목별증권사순위요청", "OPT10038", 0, screenCode);
        }

        ///<summary> 코드명:OPT10039 기능명:증권사별매매상위요청</summary>
        ///<param name="arg2">회원사코드 : 888:외국계 전체, 나머지 회원사 코드는 OPT10042 조회 또는 GetBranchCodeName()함수사용</param>
        ///<param name="arg3">거래량구분 : 0:전체, 5:5000주, 10:1만주, 50:5만주, 100:10만주, 500:50만주, 1000: 100만주</param>
        ///<param name="arg4">매매구분 : 1:순매수, 2:순매도</param>
        ///<param name="arg5">기간 : 1:전일, 5:5일, 10:10일, 60:60일</param>
        public void GetOPT10039(string callbackID, string arg2, string arg3, string arg4, string arg5)
        {
            nowCallbackID = callbackID;
            string screenCode = "0252";
            SetInputValue("화면번호", screenCode);
            SetInputValue("회원사코드", arg2);
            SetInputValue("거래량구분", arg3);
            SetInputValue("매매구분", arg4);
            SetInputValue("기간", arg5);
            CommRqData("증권사별매매상위요청", "OPT10039", 0, screenCode);
        }

        ///<summary> 코드명:OPT10040 기능명:당일주요거래원요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        public void GetOPT10040(string callbackID, string arg1)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            CommRqData("당일주요거래원요청", "OPT10040", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT10041 기능명:조기종료통화단위요청</summary>
        ///<param name="arg2">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg3">영웅클럽구분 : </param>
        public void GetOPT10041(string callbackID, string arg2, string arg3)
        {
            nowCallbackID = callbackID;
            string screenCode = "0252";
            SetInputValue("화면번호", screenCode);
            SetInputValue("종목코드", arg2);
            SetInputValue("영웅클럽구분", arg3);
            CommRqData("조기종료통화단위요청", "OPT10041", 0, screenCode);
        }

        ///<summary> 코드명:OPT10042 기능명:순매수거래원순위요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg2">시작일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg3">종료일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg4">조회기간구분 : 0:기간으로 조회, 1:시작일자, 종료일자로 조회</param>
        ///<param name="arg5">시점구분 : 0:당일, 1:전일</param>
        ///<param name="arg6">기간 : 5:5일, 10:10일, 20:20일, 40:40일, 60:60일, 120:120일</param>
        ///<param name="arg7">정렬기준 : 1:종가순, 2:날짜순</param>
        public void GetOPT10042(string callbackID, string arg1, string arg2, string arg3, string arg4, string arg5, string arg6, string arg7)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            SetInputValue("시작일자", arg2);
            SetInputValue("종료일자", arg3);
            SetInputValue("조회기간구분", arg4);
            SetInputValue("시점구분", arg5);
            SetInputValue("기간", arg6);
            SetInputValue("정렬기준", arg7);
            CommRqData("순매수거래원순위요청", "OPT10042", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT10043 기능명:거래원매물대분석요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg2">시작일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg3">종료일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg4">조회기간구분 : 0:기간으로 조회, 1:시작일자, 종료일자로 조회</param>
        ///<param name="arg5">시점구분 : 0:당일, 1:전일</param>
        ///<param name="arg6">기간 : 5:5일, 10:10일, 20:20일, 40:40일, 60:60일, 120:120일</param>
        ///<param name="arg7">정렬기준 : 1:종가순, 2:날짜순</param>
        ///<param name="arg8">회원사코드 : 888:외국계 전체, 나머지 회원사 코드는 OPT10042 조회 또는 GetBranchCodeName()함수사용</param>
        public void GetOPT10043(string callbackID, string arg1, string arg2, string arg3, string arg4, string arg5, string arg6, string arg7, string arg8)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            SetInputValue("시작일자", arg2);
            SetInputValue("종료일자", arg3);
            SetInputValue("조회기간구분", arg4);
            SetInputValue("시점구분", arg5);
            SetInputValue("기간", arg6);
            SetInputValue("정렬기준", arg7);
            SetInputValue("회원사코드", arg8);
            CommRqData("거래원매물대분석요청", "OPT10043", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT10044 기능명:일별기관매매종목요청</summary>
        ///<param name="arg2">시작일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg3">종료일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg4">매매구분 : 0:전체, 1:순매도, 2:순매수</param>
        ///<param name="arg5">시장구분 : 001:코스피, 101:코스닥</param>
        public void GetOPT10044(string callbackID, string arg2, string arg3, string arg4, string arg5)
        {
            nowCallbackID = callbackID;
            string screenCode = "0257";
            SetInputValue("화면번호", screenCode);
            SetInputValue("시작일자", arg2);
            SetInputValue("종료일자", arg3);
            SetInputValue("매매구분", arg4);
            SetInputValue("시장구분", arg5);
            CommRqData("일별기관매매종목요청", "OPT10044", 0, screenCode);
        }

        ///<summary> 코드명:OPT10045 기능명:종목별기관매매추이요청</summary>
        ///<param name="arg2">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg3">시작일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg4">종료일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg5">기관추정단가구분 : 1:매수단가, 2:매도단가</param>
        ///<param name="arg6">외인추정단가구분 : 1:매수단가, 2:매도단가</param>
        ///<param name="arg7">누적기간 : 사용안함</param>
        ///<param name="arg8">기간구분 : 사용안함</param>
        public void GetOPT10045(string callbackID, string arg2, string arg3, string arg4, string arg5, string arg6, string arg7, string arg8)
        {
            nowCallbackID = callbackID;
            string screenCode = "0258";
            SetInputValue("화면번호", screenCode);
            SetInputValue("종목코드", arg2);
            SetInputValue("시작일자", arg3);
            SetInputValue("종료일자", arg4);
            SetInputValue("기관추정단가구분", arg5);
            SetInputValue("외인추정단가구분", arg6);
            SetInputValue("누적기간", arg7);
            SetInputValue("기간구분", arg8);
            CommRqData("종목별기관매매추이요청", "OPT10045", 0, screenCode);
        }

        ///<summary> 코드명:OPT10047 기능명:체결강도추이일별요청</summary>
        ///<param name="arg2">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg3">연속조회 번호 : 0:시작, 2~n 연속조회 </param>
        public void GetOPT10047(string callbackID, string arg2 , string arg3)
        {
            nowCallbackID = callbackID;
            string screenCode = "5000";
            SetInputValue("종목코드", arg2);
            SetInputValue("틱구분", "1");
            SetInputValue("체결강도부분", "1");
            CommRqData("체결강도추이일별요청", "opt10047", Int32.Parse(arg3), screenCode);
        }

        ///<summary> 코드명:OPT10048 기능명:ELW일별민감도지표요청</summary>
        ///<param name="arg2">종목코드 : 전문 조회할 종목코드</param>
        public void GetOPT10048(string callbackID, string arg2)
        {
            nowCallbackID = callbackID;
            string screenCode = "0286";
            SetInputValue("화면번호", screenCode);
            SetInputValue("종목코드", arg2);
            CommRqData("ELW일별민감도지표요청", "OPT10048", 0, screenCode);
        }

        ///<summary> 코드명:OPT10049 기능명:ELW투자지표요청</summary>
        ///<param name="arg2">연속구분 : 연속구분</param>
        ///<param name="arg3">연속키 : 연속키</param>
        ///<param name="arg4">종목코드 : 종목코드</param>
        public void GetOPT10049(string callbackID, string arg2, string arg3, string arg4)
        {
            nowCallbackID = callbackID;
            string screenCode = "0286";
            SetInputValue("화면번호", screenCode);
            SetInputValue("연속구분", arg2);
            SetInputValue("연속키", arg3);
            SetInputValue("종목코드", arg4);
            CommRqData("ELW투자지표요청", "OPT10049", 0, screenCode);
        }

        ///<summary> 코드명:OPT10050 기능명:ELW민감도지표요청</summary>
        ///<param name="arg2">종목코드 : 전문 조회할 종목코드</param>
        public void GetOPT10050(string callbackID, string arg2)
        {
            nowCallbackID = callbackID;
            string screenCode = "0299";
            SetInputValue("화면번호", screenCode);
            SetInputValue("종목코드", arg2);
            CommRqData("ELW민감도지표요청", "OPT10050", 0, screenCode);
        }

        ///<summary> 코드명:OPT10051 기능명:업종별투자자순매수요청</summary>
        ///<param name="arg2">시장구분 : 코스피:0, 코스닥:1</param>
        ///<param name="arg3">금액수량구분 : 금액:0, 수량:1</param>
        ///<param name="arg4">기준일자 : YYYYMMDD (20170101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        public void GetOPT10051(string callbackID, string arg2, string arg3, string arg4)
        {
            nowCallbackID = callbackID;
            string screenCode = "0797";
            SetInputValue("화면번호", screenCode);
            SetInputValue("시장구분", arg2);
            SetInputValue("금액수량구분", arg3);
            SetInputValue("기준일자", arg4);
            CommRqData("업종별투자자순매수요청", "OPT10051", 0, screenCode);
        }

        ///<summary> 코드명:OPT10052 기능명:거래원순간거래량요청</summary>
        ///<param name="arg2">회원사코드 : 888:외국계 전체, 나머지 회원사 코드는 OPT10042 조회 또는 GetBranchCodeName()함수사용</param>
        ///<param name="arg3">시장구분 : 0:전체, 1:코스피, 2:코스닥, 3:종목</param>
        ///<param name="arg4">수량구분 : 0:전체, 1:1000주, 2:2000주, 3:, 5:, 10:10000주, 30: 30000주, 50: 50000주, 100: 100000주</param>
        ///<param name="arg5">가격구분 : 0:전체, 1:1천원 미만, 8:1천원 이상, 2:1천원 ~ 2천원, 3:2천원 ~ 5천원, 4:5천원 ~ 1만원, 5:1만원 이상</param>
        public void GetOPT10052(string callbackID, string arg2, string arg3, string arg4, string arg5)
        {
            nowCallbackID = callbackID;
            string screenCode = "0127";
            SetInputValue("화면번호", screenCode);
            SetInputValue("회원사코드", arg2);
            SetInputValue("시장구분", arg3);
            SetInputValue("수량구분", arg4);
            SetInputValue("가격구분", arg5);
            CommRqData("거래원순간거래량요청", "OPT10052", 0, screenCode);
        }

        ///<summary> 코드명:OPT10053 기능명:당일상위이탈원요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        public void GetOPT10053(string callbackID, string arg1)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            CommRqData("당일상위이탈원요청", "OPT10053", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT10054 기능명:변동성완화장치발동종목요청</summary>
        ///<param name="arg1">시장구분 : 000:전체, 001: 코스피, 101:코스닥</param>
        ///<param name="arg2">장전구분 : 0:전체,	1:정규시장,2:시간외단일가</param>
        ///<param name="arg3">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg4">발동구분 : 0:전체, 1:정적VI, 2:동적VI, 3:동적VI + 정적VI</param>
        ///<param name="arg5">거래량구분 : 0:사용안함, 1:사용</param>
        ///<param name="arg6">최소거래량 : 0 주 이상</param>
        ///<param name="arg7">최대거래량 : 100000000 주 이하</param>
        ///<param name="arg8">거래대금구분 : 0:사용안함, 1:사용</param>
        ///<param name="arg9">최소거래대금 : 0 백만원 이상</param>
        ///<param name="arg10">최소거래대금 : 100000000 백만원 이하</param>
        ///<param name="arg11">발동방향 : 0:전체, 1:상승, 2:하락</param>
        public void GetOPT10054(string callbackID, string arg1, string arg2, string arg3, string arg4, string arg5, string arg6, string arg7, string arg8, string arg9, string arg10, string arg11)
        {
            nowCallbackID = callbackID;
            SetInputValue("시장구분", arg1);
            SetInputValue("장전구분", arg2);
            SetInputValue("종목코드", arg3);
            SetInputValue("발동구분", arg4);
            SetInputValue("거래량구분", arg5);
            SetInputValue("최소거래량", arg6);
            SetInputValue("최대거래량", arg7);
            SetInputValue("거래대금구분", arg8);
            SetInputValue("최소거래대금", arg9);
            SetInputValue("최소거래대금", arg10);
            SetInputValue("발동방향", arg11);
            CommRqData("변동성완화장치발동종목요청", "OPT10054", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT10055 기능명:당일전일체결대량요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg2">당일전일 : 1:당일,	2:전일</param>
        public void GetOPT10055(string callbackID, string arg1, string arg2)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            SetInputValue("당일전일", arg2);
            CommRqData("당일전일체결대량요청", "OPT10055", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT10058 기능명:투자자별일별매매종목요청</summary>
        ///<param name="arg2">시작일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg3">종료일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg4">매매구분 : 전체:순매수(2)/순매도(1) 각각조회해서 비교, 순매도:1, 순매수:2</param>
        ///<param name="arg5">시장구분 : 001:코스피, 101:코스닥</param>
        ///<param name="arg6">투자자구분 : 8000:개인, 9000:외국인, 1000:금융투자, 3000:투신, 5000:기타금융, 4000:은행, 2000:보험, 6000:연기금, 7000:국가, 7100:기타법인, 9999:기관계</param>
        public void GetOPT10058(string callbackID, string arg2, string arg3, string arg4, string arg5, string arg6)
        {
            nowCallbackID = callbackID;
            string screenCode = "0795";
            SetInputValue("화면번호", screenCode);
            SetInputValue("시작일자", arg2);
            SetInputValue("종료일자", arg3);
            SetInputValue("매매구분", arg4);
            SetInputValue("시장구분", arg5);
            SetInputValue("투자자구분", arg6);
            CommRqData("투자자별일별매매종목요청", "OPT10058", 0, screenCode);
        }

        ///<summary> 코드명:OPT10059 기능명:종목별투자자기관별요청</summary>
        ///<param name="arg2">일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg3">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg4">금액수량구분 : 1:금액, 2:수량</param>
        ///<param name="arg5">매매구분 : 0:순매수, 1:매수, 2:매도</param>
        ///<param name="arg6">단위구분 : 1000:천주, 1:단주</param>
        public void GetOPT10059(string callbackID, string arg2, string arg3, string arg4, string arg5, string arg6)
        {
            nowCallbackID = callbackID;
            string screenCode = "0796";
            SetInputValue("화면번호", screenCode);
            SetInputValue("일자", arg2);
            SetInputValue("종목코드", arg3);
            SetInputValue("금액수량구분", arg4);
            SetInputValue("매매구분", arg5);
            SetInputValue("단위구분", arg6);
            CommRqData("종목별투자자기관별요청", "OPT10059", 0, screenCode);
        }

        ///<summary> 코드명:OPT10060 기능명:종목별투자자기관별차트요청</summary>
        ///<param name="arg2">일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg3">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg4">금액수량구분 : 1:금액, 2:수량</param>
        ///<param name="arg5">매매구분 : 0:순매수, 1:매수, 2:매도</param>
        ///<param name="arg6">단위구분 : 1000:천주, 1:단주</param>
        public void GetOPT10060(string callbackID, string arg2, string arg3, string arg4, string arg5, string arg6)
        {
            nowCallbackID = callbackID;
            string screenCode = "0796";
            SetInputValue("화면번호", screenCode);
            SetInputValue("일자", arg2);
            SetInputValue("종목코드", arg3);
            SetInputValue("금액수량구분", arg4);
            SetInputValue("매매구분", arg5);
            SetInputValue("단위구분", arg6);
            CommRqData("종목별투자자기관별차트요청", "OPT10060", 0, screenCode);
        }

        ///<summary> 코드명:OPT10061 기능명:종목별투자자기관별합계요청</summary>
        ///<param name="arg2">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg3">시작일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg4">종료일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg5">금액수량구분 : 1:금액, 2:수량</param>
        ///<param name="arg6">매매구분 : 0:순매수, 1:매수, 2:매도</param>
        ///<param name="arg7">단위구분 : 1000:천주, 1:단주</param>
        public void GetOPT10061(string callbackID, string arg2, string arg3, string arg4, string arg5, string arg6, string arg7)
        {
            nowCallbackID = callbackID;
            string screenCode = "0796";
            SetInputValue("화면번호", screenCode);
            SetInputValue("종목코드", arg2);
            SetInputValue("시작일자", arg3);
            SetInputValue("종료일자", arg4);
            SetInputValue("금액수량구분", arg5);
            SetInputValue("매매구분", arg6);
            SetInputValue("단위구분", arg7);
            CommRqData("종목별투자자기관별합계요청", "OPT10061", 0, screenCode);
        }

        ///<summary> 코드명:OPT10062 기능명:동일순매매순위요청</summary>
        ///<param name="arg2">시작일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg3">종료일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg4">시장구분 : 000:전체, 001: 코스피, 101:코스닥</param>
        ///<param name="arg5">매매구분 : 1:순매수, 2:순매도</param>
        ///<param name="arg6">정렬조건 : 1:수량, 2:금액</param>
        ///<param name="arg7">단위구분 : 1:단주, 1000:천주</param>
        public void GetOPT10062(string callbackID, string arg2, string arg3, string arg4, string arg5, string arg6, string arg7)
        {
            nowCallbackID = callbackID;
            string screenCode = "0798";
            SetInputValue("화면번호", screenCode);
            SetInputValue("시작일자", arg2);
            SetInputValue("종료일자", arg3);
            SetInputValue("시장구분", arg4);
            SetInputValue("매매구분", arg5);
            SetInputValue("정렬조건", arg6);
            SetInputValue("단위구분", arg7);
            CommRqData("동일순매매순위요청", "OPT10062", 0, screenCode);
        }

        ///<summary> 코드명:OPT10063 기능명:장중투자자별매매요청</summary>
        ///<param name="arg1">시장구분 : 000:전체, 001:코스피, 101:코스닥</param>
        ///<param name="arg2">금액수량구분 : 1:금액, 2:수량</param>
        ///<param name="arg3">투자자별 : 6:외국인, 7:기관계, 1:투신, 0:보험, 2:은행, 3:연기금, 4:국가, 5:기타법인</param>
        ///<param name="arg4">외국계전체 : 1:체크, 0:언체크</param>
        ///<param name="arg5">동시순매수구분 : 1:체크, 0:언체크</param>
        public void GetOPT10063(string callbackID, string arg1, string arg2, string arg3, string arg4, string arg5)
        {
            nowCallbackID = callbackID;
            SetInputValue("시장구분", arg1);
            SetInputValue("금액수량구분", arg2);
            SetInputValue("투자자별", arg3);
            SetInputValue("외국계전체", arg4);
            SetInputValue("동시순매수구분", arg5);
            CommRqData("장중투자자별매매요청", "OPT10063", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT10064 기능명:장중투자자별매매차트요청</summary>
        ///<param name="arg1">시장구분 : 000:전체, 001:코스피, 101:코스닥</param>
        ///<param name="arg2">금액수량구분 : 1:금액, 2:수량</param>
        ///<param name="arg3">매매구분 : 0:순매수, 1:매수, 2:매도</param>
        ///<param name="arg4">종목코드 : 전문 조회할 종목코드</param>
        public void GetOPT10064(string callbackID, string arg1, string arg2, string arg3, string arg4)
        {
            nowCallbackID = callbackID;
            SetInputValue("시장구분", arg1);
            SetInputValue("금액수량구분", arg2);
            SetInputValue("매매구분", arg3);
            SetInputValue("종목코드", arg4);
            CommRqData("장중투자자별매매차트요청", "OPT10064", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT10065 기능명:장중투자자별매매상위요청</summary>
        ///<param name="arg2">매매구분 : 1:순매수, 2:순매도</param>
        ///<param name="arg3">시장구분 : 000:전체, 001:코스피, 101:코스닥</param>
        ///<param name="arg4">기관구분 : 9000:외국인, 9100:외국계, 1000:금융투자, 3000:투신, 5000:기타금융, 4000:은행, 2000:보험, 6000:연기금, 7000:국가, 7100:기타법인, 9999:기관계</param>
        public void GetOPT10065(string callbackID, string arg2, string arg3, string arg4)
        {
            nowCallbackID = callbackID;
            string screenCode = "1053";
            SetInputValue("화면번호", screenCode);
            SetInputValue("매매구분", arg2);
            SetInputValue("시장구분", arg3);
            SetInputValue("기관구분", arg4);
            CommRqData("장중투자자별매매상위요청", "OPT10065", 0, screenCode);
        }

        ///<summary> 코드명:OPT10066 기능명:장중투자자별매매차트요청</summary>
        ///<param name="arg2">시장구분 : 000:전체, 001:코스피, 101:코스닥</param>
        ///<param name="arg3">금액수량구분 : 1:금액, 2:수량</param>
        ///<param name="arg4">매매구분 : 0:순매수, 1:매수, 2:매도</param>
        ///<param name="arg5">종목코드 : 전문 조회할 종목코드</param>
        public void GetOPT10066(string callbackID, string arg2, string arg3, string arg4, string arg5)
        {
            nowCallbackID = callbackID;
            string screenCode = "1054";
            SetInputValue("화면번호", screenCode);
            SetInputValue("시장구분", arg2);
            SetInputValue("금액수량구분", arg3);
            SetInputValue("매매구분", arg4);
            SetInputValue("종목코드", arg5);
            CommRqData("장중투자자별매매차트요청", "OPT10066", 0, screenCode);
        }

        ///<summary> 코드명:OPT10067 기능명:대차거래내역요청</summary>
        ///<param name="arg2">기준일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg3">시장구분 : 001:코스피, 101:코스닥</param>
        public void GetOPT10067(string callbackID, string arg2, string arg3)
        {
            nowCallbackID = callbackID;
            string screenCode = "1060";
            SetInputValue("화면번호", screenCode);
            SetInputValue("기준일자", arg2);
            SetInputValue("시장구분", arg3);
            CommRqData("대차거래내역요청", "OPT10067", 0, screenCode);
        }

        ///<summary> 코드명:OPT10068 기능명:대차거래추이요청</summary>
        ///<param name="arg2">시작일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg3">종료일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg4">전체구분 : 1: 전체표시, 0:종목코드 (지원안함. OPT20068사용).</param>
        ///<param name="arg5">종목코드 : 전문 조회할 종목코드</param>
        public void GetOPT10068(string callbackID, string arg2, string arg3, string arg4, string arg5)
        {
            nowCallbackID = callbackID;
            string screenCode = "1061";
            SetInputValue("화면번호", screenCode);
            SetInputValue("시작일자", arg2);
            SetInputValue("종료일자", arg3);
            SetInputValue("전체구분", arg4);
            SetInputValue("종목코드", arg5);
            CommRqData("대차거래추이요청", "OPT10068", 0, screenCode);
        }

        ///<summary> 코드명:OPT10069 기능명:대차거래상위10종목요청</summary>
        ///<param name="arg2">시작일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg3">종료일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg4">시장구분 : 001:코스피, 101:코스닥</param>
        public void GetOPT10069(string callbackID, string arg2, string arg3, string arg4)
        {
            nowCallbackID = callbackID;
            string screenCode = "1062";
            SetInputValue("화면번호", screenCode);
            SetInputValue("시작일자", arg2);
            SetInputValue("종료일자", arg3);
            SetInputValue("시장구분", arg4);
            CommRqData("대차거래상위10종목요청", "OPT10069", 0, screenCode);
        }

        ///<summary> 코드명:OPT10070 기능명:당일주요거래원요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        public void GetOPT10070(string callbackID, string arg1)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            CommRqData("당일주요거래원요청", "OPT10070", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT10071 기능명:시간대별전일비거래비중요청</summary>
        ///<param name="arg2">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg3">시간구분 : 1:1분,3:3분,5:5분,10:10분,15:15분,30:30분,60:60분</param>
        public void GetOPT10071(string callbackID, string arg2, string arg3)
        {
            nowCallbackID = callbackID;
            string screenCode = "0125";
            SetInputValue("화면번호", screenCode);
            SetInputValue("종목코드", arg2);
            SetInputValue("시간구분", arg3);
            CommRqData("시간대별전일비거래비중요청", "OPT10071", 0, screenCode);
        }

        ///<summary> 코드명:OPT10072 기능명:일자별종목별실현손익요청</summary>
        ///<param name="arg1">계좌번호 : 전문 조회할 보유계좌번호</param>
        ///<param name="arg2">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg3">시작일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        public void GetOPT10072(string callbackID, string arg1, string arg2, string arg3)
        {
            nowCallbackID = callbackID;
            SetInputValue("계좌번호", arg1);
            SetInputValue("종목코드", arg2);
            SetInputValue("시작일자", arg3);
            CommRqData("일자별종목별실현손익요청", "OPT10072", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT10073 기능명:일자별종목별실현손익요청</summary>
        ///<param name="arg1">계좌번호 : 전문 조회할 보유계좌번호</param>
        ///<param name="arg2">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg3">시작일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg4">종료일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        public void GetOPT10073(string callbackID, string arg1, string arg2, string arg3, string arg4)
        {
            nowCallbackID = callbackID;
            SetInputValue("계좌번호", arg1);
            SetInputValue("종목코드", arg2);
            SetInputValue("시작일자", arg3);
            SetInputValue("종료일자", arg4);
            CommRqData("일자별종목별실현손익요청", "OPT10073", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT10074 기능명:일자별실현손익요청</summary>
        ///<param name="arg2">계좌번호 : 전문 조회할 보유계좌번호</param>
        ///<param name="arg3">시작일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg4">종료일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        public void GetOPT10074(string callbackID, string arg2, string arg3, string arg4)
        {
            nowCallbackID = callbackID;
            string screenCode = "0329";
            SetInputValue("화면번호", screenCode);
            SetInputValue("계좌번호", arg2);
            SetInputValue("시작일자", arg3);
            SetInputValue("종료일자", arg4);
            CommRqData("일자별실현손익요청", "OPT10074", 0, screenCode);
        }

        ///<summary> 코드명:OPT10075 기능명:실시간미체결요청</summary>
        ///<param name="arg2">계좌번호 : 전문 조회할 보유계좌번호</param>
        ///<param name="arg3">체결구분 : 0:체결+미체결조회, 1:미체결조회, 2:체결조회</param>
        ///<param name="arg4">매매구분 : 0:전체, 1:매도, 2:매수</param>
        public void GetOPT10075(string callbackID, string arg2, string arg3, string arg4)
        {
            nowCallbackID = callbackID;
            string screenCode = "0341";
            SetInputValue("화면번호", screenCode);
            SetInputValue("계좌번호", arg2);
            SetInputValue("체결구분", arg3);
            SetInputValue("매매구분", arg4);
            CommRqData("실시간미체결요청", "OPT10075", 0, screenCode);
        }

        ///<summary> 코드명:OPT10076 기능명:실시간체결요청</summary>
        ///<param name="arg2">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg3">조회구분 : 0:전체, 1:종목</param>
        ///<param name="arg4">매매구분 : 0:전체, 1:매도, 2:매수</param>
        ///<param name="arg5">계좌번호 : 전문 조회할 보유계좌번호</param>
        ///<param name="arg6">비밀번호 : 사용안함(공백)</param>
        ///<param name="arg7">모름3 : </param>
        public void GetOPT10076(string callbackID, string arg2, string arg3, string arg4, string arg5, string arg6, string arg7)
        {
            nowCallbackID = callbackID;
            string screenCode = "0350";
            SetInputValue("화면번호", screenCode);
            SetInputValue("종목코드", arg2);
            SetInputValue("조회구분", arg3);
            SetInputValue("매매구분", arg4);
            SetInputValue("계좌번호", arg5);
            SetInputValue("비밀번호", arg6);
            SetInputValue("모름3", arg7);
            CommRqData("실시간체결요청", "OPT10076", 0, screenCode);
        }

        ///<summary> 코드명:OPT10077 기능명:당일실현손익상세요청</summary>
        ///<param name="arg2">계좌번호 : 전문 조회할 보유계좌번호</param>
        ///<param name="arg3">비밀번호 : 사용안함(공백)</param>
        ///<param name="arg4">종목코드 : 전문 조회할 종목코드</param>
        public void GetOPT10077(string callbackID, string arg2, string arg3, string arg4)
        {
            nowCallbackID = callbackID;
            string screenCode = "0355";
            SetInputValue("화면번호", screenCode);
            SetInputValue("계좌번호", arg2);
            SetInputValue("비밀번호", arg3);
            SetInputValue("종목코드", arg4);
            CommRqData("당일실현손익상세요청", "OPT10077", 0, screenCode);
        }

        ///<summary> 코드명:OPT10078 기능명:증권사별종목매매동향요청</summary>
        ///<param name="arg2">회원사코드 : 888:외국계 전체, 나머지 회원사 코드는 OPT10042 조회 또는 GetBranchCodeName()함수사용</param>
        ///<param name="arg3">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg4">시작일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg5">종료일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        public void GetOPT10078(string callbackID, string arg2, string arg3, string arg4, string arg5)
        {
            nowCallbackID = callbackID;
            string screenCode = "0350";
            SetInputValue("화면번호", screenCode);
            SetInputValue("회원사코드", arg2);
            SetInputValue("종목코드", arg3);
            SetInputValue("시작일자", arg4);
            SetInputValue("종료일자", arg5);
            CommRqData("증권사별종목매매동향요청", "OPT10078", 0, screenCode);
        }

        ///<summary> 코드명:OPT10079 기능명:주식틱차트조회요청</summary>
        ///<param name="arg2">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg3">틱범위 : 1:1틱, 3:3틱, 5:5틱, 10:10틱, 30:30틱</param>
        ///<param name="arg4">수정주가구분 : 0 or 1, 수신데이터 1:유상증자, 2:무상증자, 4:배당락, 8:액면분할, 16:액면병합, 32:기업합병, 64:감자, 256:권리락</param>
        public void GetOPT10079(string callbackID, string arg2, string arg3, string arg4)
        {
            nowCallbackID = callbackID;
            string screenCode = "0612";
            SetInputValue("화면번호", screenCode);
            SetInputValue("종목코드", arg2);
            SetInputValue("틱범위", arg3);
            SetInputValue("수정주가구분", arg4);
            CommRqData("주식틱차트조회요청", "OPT10079", 0, screenCode);
        }

        ///<summary> 코드명:OPT10080 기능명:주식분봉차트조회요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg2">틱범위 : 1:1분, 3:3분, 5:5분, 10:10분, 15:15분, 30:30분, 45:45분, 60:60분</param>
        ///<param name="arg3">수정주가구분 : 0 or 1, 수신데이터 1:유상증자, 2:무상증자, 4:배당락, 8:액면분할, 16:액면병합, 32:기업합병, 64:감자, 256:권리락</param>
        public void GetOPT10080(string callbackID, string arg1, string arg2, string arg3, string arg4)
        {
            nowCallbackID = callbackID;
            
            SetInputValue("종목코드", arg1); 
            SetInputValue("틱범위", arg2);
            SetInputValue("수정주가구분", arg3);
            logger.Info("GetOPT10080 callbackID " + callbackID + " arg1 " + arg1 + " arg2 " + arg2 + " arg3 " + arg3 + " arg4 " + arg4) ;
            CommRqData("주식분봉차트조회요청", "OPT10080", Int32.Parse(arg4), GetScrNum());
        }

        ///<summary> 코드명:OPT10081 기능명:주식일봉차트조회요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg2">기준일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg3">수정주가구분 : 0 or 1, 수신데이터 1:유상증자, 2:무상증자, 4:배당락, 8:액면분할, 16:액면병합, 32:기업합병, 64:감자, 256:권리락 (각 값은 서로 조합해서 수신될 수 있음. 예를 들면 6: 무상증자 + 배당락, 288: 기업합병+권리락)</param>
        public void GetOPT10081(string callbackID, string arg1, string arg2, string arg3, string arg4)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            SetInputValue("기준일자", arg2);
            SetInputValue("수정주가구분", arg3);
            CommRqData("주식일봉차트조회요청", "OPT10081", Int32.Parse(arg4), GetScrNum());
        }

        ///<summary> 코드명:OPT10082 기능명:주식주봉차트조회요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg2">기준일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg3">수정주가구분 : 0 or 1, 수신데이터 1:유상증자, 2:무상증자, 4:배당락, 8:액면분할, 16:액면병합, 32:기업합병, 64:감자, 256:권리락</param>
        public void GetOPT10082(string callbackID, string arg1, string arg2, string arg3)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            SetInputValue("기준일자", arg2);
            SetInputValue("수정주가구분", arg3);
            CommRqData("주식주봉차트조회요청", "OPT10082", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT10083 기능명:주식월봉차트조회요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg2">기준일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg3">수정주가구분 : 0 or 1, 수신데이터 1:유상증자, 2:무상증자, 4:배당락, 8:액면분할, 16:액면병합, 32:기업합병, 64:감자, 256:권리락</param>
        public void GetOPT10083(string callbackID, string arg1, string arg2, string arg3)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            SetInputValue("기준일자", arg2);
            SetInputValue("수정주가구분", arg3);
            CommRqData("주식월봉차트조회요청", "OPT10083", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT10084 기능명:당일전일체결요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg2">당일전일 : 당일 : 1, 전일 : 2</param>
        ///<param name="arg3">틱분 : 틱 : 0 , 분 : 1</param>
        ///<param name="arg4">시간 : 조회시간 4자리, 오전 9시일 경우 '0900', 오후 2시 30분일 경우 '1430'</param>
        public void GetOPT10084(string callbackID, string arg1, string arg2, string arg3, string arg4)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            SetInputValue("당일전일", arg2);
            SetInputValue("틱분", arg3);
            SetInputValue("시간", arg4);
            CommRqData("당일전일체결요청", "OPT10084", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT10085 기능명:계좌수익률요청</summary>
        ///<param name="arg1">계좌번호 : 전문 조회할 보유계좌번호</param>
        public void GetOPT10085(string callbackID, string arg1)
        {
            nowCallbackID = callbackID;
            SetInputValue("계좌번호", arg1);
            CommRqData("계좌수익률요청", "OPT10085", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT10086 기능명:일별주가요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg2">조회일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg3">표시구분 : 0:수량, 1:금액(백만원)</param>
        public void GetOPT10086(string callbackID, string arg1, string arg2, string arg3)
        {
            nowCallbackID = callbackID;

            SetInputValue("종목코드", arg1); 
            SetInputValue("조회일자", arg2);
            SetInputValue("표시구분", arg3);
            CommRqData("일별주가요청", "OPT10086", 0, "0101");
        }

        ///<summary> 코드명:OPT10087 기능명:시간외단일가요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        public void GetOPT10087(string callbackID, string arg1)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            CommRqData("시간외단일가요청", "OPT10087", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT10094 기능명:주식년봉차트조회요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg2">기준일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg3">끝일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg4">수정주가구분 : 0 or 1, 수신데이터 1:유상증자, 2:무상증자, 4:배당락, 8:액면분할, 16:액면병합, 32:기업합병, 64:감자, 256:권리락</param>
        public void GetOPT10094(string callbackID, string arg1, string arg2, string arg3, string arg4)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            SetInputValue("기준일자", arg2);
            SetInputValue("끝일자", arg3);
            SetInputValue("수정주가구분", arg4);
            CommRqData("주식년봉차트조회요청", "OPT10094", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT20001 기능명:업종현재가요청</summary>
        ///<param name="arg2">시장구분 : 0:코스피, 1:코스닥, 2:코스피200</param>
        ///<param name="arg3">업종코드 : 001:종합(KOSPI), 002:대형주, 003:중형주, 004:소형주 101:종합(KOSDAQ), 201:KOSPI200, 302:KOSTAR, 701: KRX100 나머지 ※ 업종코드 참고</param>
        public void GetOPT20001(string callbackID, string arg2, string arg3)
        {
            nowCallbackID = callbackID;
            string screenCode = "0211";
            SetInputValue("화면번호", screenCode);
            SetInputValue("시장구분", arg2);
            SetInputValue("업종코드", arg3);
            CommRqData("업종현재가요청", "OPT20001", 0, screenCode);
        }

        ///<summary> 코드명:OPT20002 기능명:업종별주가요청</summary>
        ///<param name="arg2">시장구분 : 0:코스피, 1:코스닥, 2:코스피200</param>
        ///<param name="arg3">업종코드 : 001:종합(KOSPI), 002:대형주, 003:중형주, 004:소형주 101:종합(KOSDAQ), 201:KOSPI200, 302:KOSTAR, 701: KRX100 나머지 ※ 업종코드 참고</param>
        public void GetOPT20002(string callbackID, string arg2, string arg3)
        {
            nowCallbackID = callbackID;
            string screenCode = "0213";
            SetInputValue("화면번호", screenCode);
            SetInputValue("시장구분", arg2);
            SetInputValue("업종코드", arg3);
            CommRqData("업종별주가요청", "OPT20002", 0, screenCode);
        }

        ///<summary> 코드명:OPT20003 기능명:전업종지수요청</summary>
        ///<param name="arg2">업종코드 : 001:종합(KOSPI), 002:대형주, 003:중형주, 004:소형주 101:종합(KOSDAQ), 201:KOSPI200, 302:KOSTAR, 701: KRX100 나머지 ※ 업종코드 참고</param>
        public void GetOPT20003(string callbackID, string arg2)
        {
            nowCallbackID = callbackID;
            string screenCode = "0214";
            SetInputValue("화면번호", screenCode);
            SetInputValue("업종코드", arg2);
            CommRqData("전업종지수요청", "OPT20003", 0, screenCode);
        }

        ///<summary> 코드명:OPT20004 기능명:업종틱차트조회요청</summary>
        ///<param name="arg1">업종코드 : 001:종합(KOSPI), 002:대형주, 003:중형주, 004:소형주 101:종합(KOSDAQ), 201:KOSPI200, 302:KOSTAR, 701: KRX100 나머지 ※ 업종코드 참고</param>
        ///<param name="arg2">틱범위 : 1:1틱, 3:3틱, 5:5틱, 10:10틱, 30:30틱</param>
        ///<param name="arg3">수정주가구분 : 0 or 1</param>
        public void GetOPT20004(string callbackID, string arg1, string arg2, string arg3)
        {
            nowCallbackID = callbackID;
            SetInputValue("업종코드", arg1);
            SetInputValue("틱범위", arg2);
            SetInputValue("수정주가구분", arg3);
            CommRqData("업종틱차트조회요청", "OPT20004", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT20005 기능명:업종분봉조회요청</summary>
        ///<param name="arg1">업종코드 : 001:종합(KOSPI), 002:대형주, 003:중형주, 004:소형주 101:종합(KOSDAQ), 201:KOSPI200, 302:KOSTAR, 701: KRX100 나머지 ※ 업종코드 참고</param>
        ///<param name="arg2">틱범위 : 1:1틱, 3:3틱, 5:5틱, 10:10틱, 30:30틱</param>
        ///<param name="arg3">수정주가구분 : 0 or 1</param>
        public void GetOPT20005(string callbackID, string arg1, string arg2, string arg3)
        {
            nowCallbackID = callbackID;
            SetInputValue("업종코드", arg1);
            SetInputValue("틱범위", arg2);
            SetInputValue("수정주가구분", arg3);
            CommRqData("업종분봉조회요청", "OPT20005", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT20006 기능명:업종일봉조회요청</summary>
        ///<param name="arg1">업종코드 : 001:종합(KOSPI), 002:대형주, 003:중형주, 004:소형주 101:종합(KOSDAQ), 201:KOSPI200, 302:KOSTAR, 701: KRX100 나머지 ※ 업종코드 참고</param>
        ///<param name="arg2">기준일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg3">수정주가구분 : 0 or 1</param>
        public void GetOPT20006(string callbackID, string arg1, string arg2, string arg3)
        {
            nowCallbackID = callbackID;
            SetInputValue("업종코드", arg1);
            SetInputValue("기준일자", arg2);
            SetInputValue("수정주가구분", arg3);
            CommRqData("업종일봉조회요청", "OPT20006", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT20007 기능명:업종주봉조회요청</summary>
        ///<param name="arg1">업종코드 : 001:종합(KOSPI), 002:대형주, 003:중형주, 004:소형주 101:종합(KOSDAQ), 201:KOSPI200, 302:KOSTAR, 701: KRX100 나머지 ※ 업종코드 참고</param>
        ///<param name="arg2">기준일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg3">수정주가구분 : 0 or 1</param>
        public void GetOPT20007(string callbackID, string arg1, string arg2, string arg3)
        {
            nowCallbackID = callbackID;
            SetInputValue("업종코드", arg1);
            SetInputValue("기준일자", arg2);
            SetInputValue("수정주가구분", arg3);
            CommRqData("업종주봉조회요청", "OPT20007", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT20008 기능명:업종월봉조회요청</summary>
        ///<param name="arg1">업종코드 : 001:종합(KOSPI), 002:대형주, 003:중형주, 004:소형주 101:종합(KOSDAQ), 201:KOSPI200, 302:KOSTAR, 701: KRX100 나머지 ※ 업종코드 참고</param>
        ///<param name="arg2">기준일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg3">수정주가구분 : 0 or 1</param>
        public void GetOPT20008(string callbackID, string arg1, string arg2, string arg3)
        {
            nowCallbackID = callbackID;
            SetInputValue("업종코드", arg1);
            SetInputValue("기준일자", arg2);
            SetInputValue("수정주가구분", arg3);
            CommRqData("업종월봉조회요청", "OPT20008", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT20009 기능명:업종현재가일별요청</summary>
        ///<param name="arg1">시장구분 : P00101:코스피, P10102:코스닥</param>
        ///<param name="arg2">업종코드 : 001:종합(KOSPI), 002:대형주, 003:중형주, 004:소형주 101:종합(KOSDAQ), 201:KOSPI200, 302:KOSTAR, 701: KRX100 나머지 ※ 업종코드 참고</param>
        public void GetOPT20009(string callbackID, string arg1, string arg2)
        {
            nowCallbackID = callbackID;
            SetInputValue("시장구분", arg1);
            SetInputValue("업종코드", arg2);
            CommRqData("업종현재가일별요청", "OPT20009", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT20019 기능명:업종년봉조회요청</summary>
        ///<param name="arg1">업종코드 : 001:종합(KOSPI), 002:대형주, 003:중형주, 004:소형주 101:종합(KOSDAQ), 201:KOSPI200, 302:KOSTAR, 701: KRX100 나머지 ※ 업종코드 참고</param>
        ///<param name="arg2">기준일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg3">수정주가구분 : 0 or 1</param>
        public void GetOPT20019(string callbackID, string arg1, string arg2, string arg3)
        {
            nowCallbackID = callbackID;
            SetInputValue("업종코드", arg1);
            SetInputValue("기준일자", arg2);
            SetInputValue("수정주가구분", arg3);
            CommRqData("업종년봉조회요청", "OPT20019", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT20068 기능명:대차거래추이요청(종목별)</summary>
        ///<param name="arg2">시작일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg3">종료일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg4">전체구분 : 0:종목코드 입력종목만 표시, 1: 전체표시(지원안함. OPT10068사용).</param>
        ///<param name="arg5">종목코드 : 전문 조회할 종목코드</param>
        public void GetOPT20068(string callbackID, string arg2, string arg3, string arg4, string arg5)
        {
            nowCallbackID = callbackID;
            string screenCode = "1061";
            SetInputValue("화면번호", screenCode);
            SetInputValue("시작일자", arg2);
            SetInputValue("종료일자", arg3);
            SetInputValue("전체구분", arg4);
            SetInputValue("종목코드", arg5);
            CommRqData("대차거래추이요청(종목별)", "OPT20068", 0, screenCode);
        }

        ///<summary> 코드명:OPT30001 기능명:ELW가격급등락요청</summary>
        ///<param name="arg2">등락구분 : 1:급등, 2:급락</param>
        ///<param name="arg3">시간구분 : 1:분전, 2:일전</param>
        ///<param name="arg4">시간 : </param>
        ///<param name="arg5">거래량구분 : 0:전체, 10:만주이상, 50:5만주이상, 100:10만주이상, 300:30만주이상, 500:50만주이상, 1000:백만주이상</param>
        ///<param name="arg6">발행사코드 : ※ 발행사코드 참고 </param>
        ///<param name="arg7">기초자산코드 : ※ 기초자산코드 참고</param>
        ///<param name="arg8">권리구분 : 000:전체, 001:콜, 002:풋, 003:DC, 004:DP, 005:EX, 006:조기종료콜, 007:조기종료풋</param>
        ///<param name="arg9">LP코드 : ※ LP코드 참고</param>
        ///<param name="arg10">거래종료ELW제외 : </param>
        public void GetOPT30001(string callbackID, string arg2, string arg3, string arg4, string arg5, string arg6, string arg7, string arg8, string arg9, string arg10)
        {
            nowCallbackID = callbackID;
            string screenCode = "0272";
            SetInputValue("화면번호", screenCode);
            SetInputValue("등락구분", arg2);
            SetInputValue("시간구분", arg3);
            SetInputValue("시간", arg4);
            SetInputValue("거래량구분", arg5);
            SetInputValue("발행사코드", arg6);
            SetInputValue("기초자산코드", arg7);
            SetInputValue("권리구분", arg8);
            SetInputValue("LP코드", arg9);
            SetInputValue("거래종료ELW제외", arg10);
            CommRqData("ELW가격급등락요청", "OPT30001", 0, screenCode);
        }

        ///<summary> 코드명:OPT30002 기능명:거래원별ELW순매매상위요청</summary>
        ///<param name="arg2">발행사코드 : ※ 발행사코드 참고 </param>
        ///<param name="arg3">거래량구분 : 0:전체, 5:5천주, 10:만주, 50:5만주, 100:10만주, 500:50만주, 1000:백만주</param>
        ///<param name="arg4">매매구분 : 1:순매수, 2:순매도</param>
        ///<param name="arg5">기간 : 1:전일, 5:5일, 10:10일, 40:40일, 60:60일</param>
        ///<param name="arg6">거래종료ELW제외 : </param>
        public void GetOPT30002(string callbackID, string arg2, string arg3, string arg4, string arg5, string arg6)
        {
            nowCallbackID = callbackID;
            string screenCode = "0273";
            SetInputValue("화면번호", screenCode);
            SetInputValue("발행사코드", arg2);
            SetInputValue("거래량구분", arg3);
            SetInputValue("매매구분", arg4);
            SetInputValue("기간", arg5);
            SetInputValue("거래종료ELW제외", arg6);
            CommRqData("거래원별ELW순매매상위요청", "OPT30002", 0, screenCode);
        }

        ///<summary> 코드명:OPT30003 기능명:ELW LP보유일별추이요청</summary>
        ///<param name="arg2">기초자산코드 : ※ 기초자산코드 참고</param>
        ///<param name="arg3">기준일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        public void GetOPT30003(string callbackID, string arg2, string arg3)
        {
            nowCallbackID = callbackID;
            string screenCode = "0285";
            SetInputValue("화면번호", screenCode);
            SetInputValue("기초자산코드", arg2);
            SetInputValue("기준일자", arg3);
            CommRqData("ELW LP보유일별추이요청", "OPT30003", 0, screenCode);
        }

        ///<summary> 코드명:OPT30004 기능명:ELW괴리율요청</summary>
        ///<param name="arg2">발행사코드 : ※ 발행사 코드 참고</param>
        ///<param name="arg3">기초자산코드 : ※ 기초자산 코드 참고</param>
        ///<param name="arg4">권리구분 : 000: 전체, 001: 콜, 002: 풋, 003: DC, 004: DP, 005: EX, 006: 조기종료콜, 007: 조기종료풋</param>
        ///<param name="arg5">LP코드 : ※ LP코드 참고</param>
        ///<param name="arg6">거래종료ELW제외 : 1:거래종료ELW제외, 0:거래종료ELW포함</param>
        public void GetOPT30004(string callbackID, string arg2, string arg3, string arg4, string arg5, string arg6)
        {
            nowCallbackID = callbackID;
            string screenCode = "0287";
            SetInputValue("화면번호", screenCode);
            SetInputValue("발행사코드", arg2);
            SetInputValue("기초자산코드", arg3);
            SetInputValue("권리구분", arg4);
            SetInputValue("LP코드", arg5);
            SetInputValue("거래종료ELW제외", arg6);
            CommRqData("ELW괴리율요청", "OPT30004", 0, screenCode);
        }

        ///<summary> 코드명:OPT30005 기능명:ELW조건검색요청</summary>
        ///<param name="arg1">발행사코드 : ※ 발행사코드 참고 12자리 (키움증권 : "         500")</param>
        ///<param name="arg2">기초자산코드 : ※ 기초자산코드 (하이닉스 : "A000660")</param>
        ///<param name="arg3">권리구분 : 0:전체, 1:콜, 2:풋, 3:DC, 4:DP, 5:EX, 6:조기종료콜, 7:조기종료풋</param>
        ///<param name="arg4">LP코드 : ※ 발행사코드 참고 3~4자리 (키움증권 : "500")</param>
        ///<param name="arg5">정렬구분 : 0:정렬없음, 1:상승율순, 2:상승폭순, 3:하락율순, 4:하락폭순, 5:거래량순, 6:거래대금순, 7:잔존일순</param>
        public void GetOPT30005(string callbackID, string arg1, string arg2, string arg3, string arg4, string arg5)
        {
            nowCallbackID = callbackID;
            SetInputValue("발행사코드", arg1);
            SetInputValue("기초자산코드", arg2);
            SetInputValue("권리구분", arg3);
            SetInputValue("LP코드", arg4);
            SetInputValue("정렬구분", arg5);
            CommRqData("ELW조건검색요청", "OPT30005", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT30007 기능명:ELW종목상세요청</summary>
        ///<param name="arg2">발행사코드 : ※ 발행사코드 참고</param>
        ///<param name="arg3">기초자산코드 : ※ 기초자산코드 참고</param>
        ///<param name="arg4">권리구분 : 000:전체, 001:콜, 002:풋, 003:DC, 004:DP, 006:조기종료콜, 007:조기종료풋</param>
        ///<param name="arg5">LP코드 : ※ LP코드 참고</param>
        ///<param name="arg6">정렬구분 : </param>
        ///<param name="arg7">거래종료ELW제외 : 1:거래종료제외, 0:거래종료포함</param>
        public void GetOPT30007(string callbackID, string arg2, string arg3, string arg4, string arg5, string arg6, string arg7)
        {
            nowCallbackID = callbackID;
            string screenCode = "0290";
            SetInputValue("화면번호", screenCode);
            SetInputValue("발행사코드", arg2);
            SetInputValue("기초자산코드", arg3);
            SetInputValue("권리구분", arg4);
            SetInputValue("LP코드", arg5);
            SetInputValue("정렬구분", arg6);
            SetInputValue("거래종료ELW제외", arg7);
            CommRqData("ELW종목상세요청", "OPT30007", 0, screenCode);
        }

        ///<summary> 코드명:OPT30008 기능명:ELW민감도지표요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        public void GetOPT30008(string callbackID, string arg1)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            CommRqData("ELW민감도지표요청", "OPT30008", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT30009 기능명:ELW등락율순위요청</summary>
        ///<param name="arg2">정렬구분 : 1:상승률, 2:상승폭, 3:하락률, 4:하락폭</param>
        ///<param name="arg3">권리구분 : 000:전체, 001:콜, 002:풋, 003:DC, 004:DP, 006:조기종료콜, 007:조기종료풋</param>
        ///<param name="arg4">거래종료제외 : 1:거래종료제외, 0:거래종료포함</param>
        public void GetOPT30009(string callbackID, string arg2, string arg3, string arg4)
        {
            nowCallbackID = callbackID;
            string screenCode = "0293";
            SetInputValue("화면번호", screenCode);
            SetInputValue("정렬구분", arg2);
            SetInputValue("권리구분", arg3);
            SetInputValue("거래종료제외", arg4);
            CommRqData("ELW등락율순위요청", "OPT30009", 0, screenCode);
        }

        ///<summary> 코드명:OPT30010 기능명:ELW잔량순위요청</summary>
        ///<param name="arg2">정렬구분 : 1:순매수잔량상위, 2: 순매도 잔량상위</param>
        ///<param name="arg3">권리구분 : 000: 전체, 001: 콜, 002: 풋, 003: DC, 004: DP, 006: 조기종료콜, 007: 조기종료풋</param>
        ///<param name="arg4">거래종료제외 : 1:거래종료제외, 0:거래종료포함</param>
        public void GetOPT30010(string callbackID, string arg2, string arg3, string arg4)
        {
            nowCallbackID = callbackID;
            string screenCode = "0294";
            SetInputValue("화면번호", screenCode);
            SetInputValue("정렬구분", arg2);
            SetInputValue("권리구분", arg3);
            SetInputValue("거래종료제외", arg4);
            CommRqData("ELW잔량순위요청", "OPT30010", 0, screenCode);
        }

        ///<summary> 코드명:OPT30011 기능명:ELW근접율요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        public void GetOPT30011(string callbackID, string arg1)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            CommRqData("ELW근접율요청", "OPT30011", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT30012 기능명:ELW종목상세정보요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        public void GetOPT30012(string callbackID, string arg1)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            CommRqData("ELW종목상세정보요청", "OPT30012", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT40001 기능명:ETF수익율요청</summary>
        ///<param name="arg2">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg3">사용안함 : "201" or 공백</param>
        ///<param name="arg4">기간 : 0:1주, 1:1달, 2:6개월, 3:1년</param>
        public void GetOPT40001(string callbackID, string arg2, string arg3, string arg4)
        {
            nowCallbackID = callbackID;
            string screenCode = "0275";
            SetInputValue("화면번호", screenCode);
            SetInputValue("종목코드", arg2);
            SetInputValue("사용안함", arg3);
            SetInputValue("기간", arg4);
            CommRqData("ETF수익율요청", "OPT40001", 0, screenCode);
        }

        ///<summary> 코드명:OPT40002 기능명:ETF종목정보요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        public void GetOPT40002(string callbackID, string arg1)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            CommRqData("ETF종목정보요청", "OPT40002", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT40003 기능명:ETF일별추이요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        public void GetOPT40003(string callbackID, string arg1)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            CommRqData("ETF일별추이요청", "OPT40003", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT40004 기능명:ETF전체시세요청</summary>
        ///<param name="arg2">과세유형 : 0:전체, 1:수익증권형 국내주식, 2:수익증권형, 3:회사형, 4:외국</param>
        ///<param name="arg3">NAV대비 : 0:전체, 1:NAV > 전일종가, 2:NAV < 전일종가</param>
        ///<param name="arg4">운용사 : 000000:전체, 410100:KODEX(삼성), 410260:KOSEF(우리), 410810:TIGER(미래에셋), 410200:KINDEX(한국투자), 410223:KStar(KB), 410220:아리랑(한화), 999999:기타운용사</param>
        ///<param name="arg5">과세여부 : 0:전체, 1:거래세비과세, 2:보유기간비과세, 3:거래세과세, 4:보유기간과세</param>
        ///<param name="arg6">추적지수 : ※  </param>
        public void GetOPT40004(string callbackID, string arg2, string arg3, string arg4, string arg5, string arg6)
        {
            nowCallbackID = callbackID;
            string screenCode = "0278";
            SetInputValue("화면번호", screenCode);
            SetInputValue("과세유형", arg2);
            SetInputValue("NAV대비", arg3);
            SetInputValue("운용사", arg4);
            SetInputValue("과세여부", arg5);
            SetInputValue("추적지수", arg6);
            CommRqData("ETF전체시세요청", "OPT40004", 0, screenCode);
        }

        ///<summary> 코드명:OPT40005 기능명:ETF일별추이요청</summary>
        ///<param name="arg2">종목코드 : 전문 조회할 종목코드</param>
        public void GetOPT40005(string callbackID, string arg2)
        {
            nowCallbackID = callbackID;
            string screenCode = "0279";
            SetInputValue("화면번호", screenCode);
            SetInputValue("종목코드", arg2);
            CommRqData("ETF일별추이요청", "OPT40005", 0, screenCode);
        }

        ///<summary> 코드명:OPT40006 기능명:ETF시간대별추이요청</summary>
        ///<param name="arg2">과세유형 : 0:전체, 1:수익증권형 국내주식, 2:수익증권형, 3:회사형, 4:외국</param>
        ///<param name="arg3">NAV대비 : 0:전체, 1:NAV > 전일종가, 2:NAV < 전일종가</param>
        ///<param name="arg4">운용사 : 000000:전체, 410100:KODEX(삼성), 410260:KOSEF(우리), 410810:TIGER(미래에셋), 410200:KINDEX(한국투자), 410223:KStar(KB), 410220:아리랑(한화), 999999:기타운용사</param>
        ///<param name="arg5">과세여부 : 0:전체, 1:거래세비과세, 2:보유기간비과세, 3:거래세과세, 4:보유기간과세</param>
        ///<param name="arg6">추적지수 : ※  </param>
        public void GetOPT40006(string callbackID, string arg2, string arg3, string arg4, string arg5, string arg6)
        {
            nowCallbackID = callbackID;
            string screenCode = "0279";
            SetInputValue("화면번호", screenCode);
            SetInputValue("과세유형", arg2);
            SetInputValue("NAV대비", arg3);
            SetInputValue("운용사", arg4);
            SetInputValue("과세여부", arg5);
            SetInputValue("추적지수", arg6);
            CommRqData("ETF시간대별추이요청", "OPT40006", 0, screenCode);
        }

        ///<summary> 코드명:OPT40007 기능명:ETF시간대별체결요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        public void GetOPT40007(string callbackID, string arg1)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            CommRqData("ETF시간대별체결요청", "OPT40007", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT40008 기능명:ETF시간대별체결요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        public void GetOPT40008(string callbackID, string arg1)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            CommRqData("ETF시간대별체결요청", "OPT40008", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT40009 기능명:ETF시간대별체결요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        public void GetOPT40009(string callbackID, string arg1)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            CommRqData("ETF시간대별체결요청", "OPT40009", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT40010 기능명:ETF시간대별추이요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        public void GetOPT40010(string callbackID, string arg1)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            CommRqData("ETF시간대별추이요청", "OPT40010", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50001 기능명:선옵현재가정보요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        public void GetOPT50001(string callbackID, string arg1)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            CommRqData("선옵현재가정보요청", "OPT50001", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50002 기능명:선옵일자별체결요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        public void GetOPT50002(string callbackID, string arg1)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            CommRqData("선옵일자별체결요청", "OPT50002", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50003 기능명:선옵시고저가요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        public void GetOPT50003(string callbackID, string arg1)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            CommRqData("선옵시고저가요청", "OPT50003", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50004 기능명:콜옵션행사가요청</summary>
        ///<param name="arg1">만기년월 : YYYYMM, 6자리</param>
        public void GetOPT50004(string callbackID, string arg1)
        {
            nowCallbackID = callbackID;
            SetInputValue("만기년월", arg1);
            CommRqData("콜옵션행사가요청", "OPT50004", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50005 기능명:선옵시간별거래량요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        public void GetOPT50005(string callbackID, string arg1)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            CommRqData("선옵시간별거래량요청", "OPT50005", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50006 기능명:선옵체결추이요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        public void GetOPT50006(string callbackID, string arg1)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            CommRqData("선옵체결추이요청", "OPT50006", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50007 기능명:선물시세추이요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg2">시간단위 : 00:틱, 1:1분, 5:5분,10:10분,15:15분,30:30분,0:일</param>
        ///<param name="arg3">시간검색 : 지원안함</param>
        public void GetOPT50007(string callbackID, string arg1, string arg2, string arg3)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            SetInputValue("시간단위", arg2);
            SetInputValue("시간검색", arg3);
            CommRqData("선물시세추이요청", "OPT50007", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50008 기능명:프로그램매매추이차트요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg2">시간구분 : 1만 가능</param>
        public void GetOPT50008(string callbackID, string arg1, string arg2)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            SetInputValue("시간구분", arg2);
            CommRqData("프로그램매매추이차트요청", "OPT50008", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50009 기능명:선옵시간별잔량요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg2">시간구분 : 1만 가능</param>
        public void GetOPT50009(string callbackID, string arg1, string arg2)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            SetInputValue("시간구분", arg2);
            CommRqData("선옵시간별잔량요청", "OPT50009", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50010 기능명:선옵호가잔량추이요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg2">시간구분 : 1만 가능</param>
        public void GetOPT50010(string callbackID, string arg1, string arg2)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            SetInputValue("시간구분", arg2);
            CommRqData("선옵호가잔량추이요청", "OPT50010", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50011 기능명:선옵호가잔량추이요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg2">시간단위  :  30초:00, 1분:01, 5분:05, 10분:10, 30분:30, 1일:99</param>
        ///<param name="arg3">시간구분 : 1만 가능</param>
        public void GetOPT50011(string callbackID, string arg1, string arg2, string arg3)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            SetInputValue("시간단위 ", arg2);
            SetInputValue("시간구분", arg3);
            CommRqData("선옵호가잔량추이요청", "OPT50011", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50012 기능명:선옵타임스프레드차트요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg2">시간단위 : 30초:00, 1분:01, 5분:05, 10분:10, 30분:30, 1일:99</param>
        public void GetOPT50012(string callbackID, string arg1, string arg2)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            SetInputValue("시간단위", arg2);
            CommRqData("선옵타임스프레드차트요청", "OPT50012", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50013 기능명:선물가격대별비중차트요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg2">봉갯수 : 봉갯수</param>
        public void GetOPT50013(string callbackID, string arg1, string arg2)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            SetInputValue("봉갯수", arg2);
            CommRqData("선물가격대별비중차트요청", "OPT50013", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50014 기능명:선물가격대별비중차트요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg2">봉갯수 : 봉갯수</param>
        public void GetOPT50014(string callbackID, string arg1, string arg2)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            SetInputValue("봉갯수", arg2);
            CommRqData("선물가격대별비중차트요청", "OPT50014", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50015 기능명:선물미결제약정일차트요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg2">시간단위 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        public void GetOPT50015(string callbackID, string arg1, string arg2)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            SetInputValue("시간단위", arg2);
            CommRqData("선물미결제약정일차트요청", "OPT50015", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50016 기능명:베이시스추이차트요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg2">시간단위 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        public void GetOPT50016(string callbackID, string arg1, string arg2)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            SetInputValue("시간단위", arg2);
            CommRqData("베이시스추이차트요청", "OPT50016", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50017 기능명:베이시스추이차트요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg2">시간단위 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        public void GetOPT50017(string callbackID, string arg1, string arg2)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            SetInputValue("시간단위", arg2);
            CommRqData("베이시스추이차트요청", "OPT50017", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50018 기능명:풋콜옵션비율차트요청</summary>
        ///<param name="arg1">시간단위 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        public void GetOPT50018(string callbackID, string arg1)
        {
            nowCallbackID = callbackID;
            SetInputValue("시간단위", arg1);
            CommRqData("풋콜옵션비율차트요청", "OPT50018", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50019 기능명:선물옵션현재가정보요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        public void GetOPT50019(string callbackID, string arg1)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            CommRqData("선물옵션현재가정보요청", "OPT50019", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50020 기능명:복수종목결제월별시세요청</summary>
        ///<param name="arg1">만기연월 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        public void GetOPT50020(string callbackID, string arg1)
        {
            nowCallbackID = callbackID;
            SetInputValue("만기연월", arg1);
            CommRqData("복수종목결제월별시세요청", "OPT50020", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50021 기능명:콜종목결제월별시세요청</summary>
        ///<param name="arg1">만기연월 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        public void GetOPT50021(string callbackID, string arg1)
        {
            nowCallbackID = callbackID;
            SetInputValue("만기연월", arg1);
            CommRqData("콜종목결제월별시세요청", "OPT50021", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50022 기능명:풋종목결제월별시세요청</summary>
        ///<param name="arg1">만기연월 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        public void GetOPT50022(string callbackID, string arg1)
        {
            nowCallbackID = callbackID;
            SetInputValue("만기연월", arg1);
            CommRqData("풋종목결제월별시세요청", "OPT50022", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50023 기능명:민감도지표추이요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg2">시간구분 : 분 단위시간 입력(예 1:1분, 3:3분, 5:5분, 10:10분, 30:30분)</param>
        public void GetOPT50023(string callbackID, string arg1, string arg2)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            SetInputValue("시간구분", arg2);
            CommRqData("민감도지표추이요청", "OPT50023", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50024 기능명:일별변동성분석그래프요청</summary>
        ///<param name="arg1">역사적변동성1 : 역사적변동성1</param>
        ///<param name="arg2">역사적변동성2 : 역사적변동성2</param>
        ///<param name="arg3">역사적변동성3 : 역사적변동성3</param>
        ///<param name="arg4">기간 : 5:5일, 10:10일, 20:20일, 60:60일, 250:250일, 250일까지 입력가능</param>
        public void GetOPT50024(string callbackID, string arg1, string arg2, string arg3, string arg4)
        {
            nowCallbackID = callbackID;
            SetInputValue("역사적변동성1", arg1);
            SetInputValue("역사적변동성2", arg2);
            SetInputValue("역사적변동성3", arg3);
            SetInputValue("기간", arg4);
            CommRqData("일별변동성분석그래프요청", "OPT50024", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50025 기능명:시간별변동성분석그래프요청</summary>
        ///<param name="arg1">역사적변동성1 : 역사적변동성1</param>
        ///<param name="arg2">역사적변동성2 : 역사적변동성2</param>
        ///<param name="arg3">역사적변동성3 : 역사적변동성3</param>
        ///<param name="arg4">기간 : 5:5일, 10:10일, 20:20일, 60:60일, 250:250일, 250일까지 입력가능</param>
        public void GetOPT50025(string callbackID, string arg1, string arg2, string arg3, string arg4)
        {
            nowCallbackID = callbackID;
            SetInputValue("역사적변동성1", arg1);
            SetInputValue("역사적변동성2", arg2);
            SetInputValue("역사적변동성3", arg3);
            SetInputValue("기간", arg4);
            CommRqData("시간별변동성분석그래프요청", "OPT50025", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50026 기능명:선옵주문체결요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg2">조회구분 : "1"</param>
        ///<param name="arg3">매매구분 : 0:전체, 1:매도, 2:매수</param>
        ///<param name="arg4">체결구분 : 0:전체, 1:미체결내역, 2:체결내역</param>
        public void GetOPT50026(string callbackID, string arg1, string arg2, string arg3, string arg4)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            SetInputValue("조회구분", arg2);
            SetInputValue("매매구분", arg3);
            SetInputValue("체결구분", arg4);
            CommRqData("선옵주문체결요청", "OPT50026", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50027 기능명:선옵잔고요청</summary>
        ///<param name="arg1">계좌번호 : 전문 조회할 보유계좌번호</param>
        public void GetOPT50027(string callbackID, string arg1)
        {
            nowCallbackID = callbackID;
            SetInputValue("계좌번호", arg1);
            CommRqData("선옵잔고요청", "OPT50027", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50028 기능명:선물틱차트요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg2">시간단위 : 1틱:1, 3틱:3, 5:5틱:5, 10틱:10, 20틱:20, 30틱:30, 60틱:60, 120틱:120</param>
        public void GetOPT50028(string callbackID, string arg1, string arg2)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            SetInputValue("시간단위", arg2);
            CommRqData("선물틱차트요청", "OPT50028", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50029 기능명:선물분차트요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg2">시간단위 : 1분:1, 3분:3, 5분:5, 10분:10, 15분:15, 30분:30, 60분:60, 120분:120</param>
        public void GetOPT50029(string callbackID, string arg1, string arg2)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            SetInputValue("시간단위", arg2);
            CommRqData("선물분차트요청", "OPT50029", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50030 기능명:선물일차트요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg2">기준일자 : YYYYMM, 6자리</param>
        public void GetOPT50030(string callbackID, string arg1, string arg2)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            SetInputValue("기준일자", arg2);
            CommRqData("선물일차트요청", "OPT50030", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50031 기능명:선옵잔고손익요청</summary>
        ///<param name="arg1">계좌번호 : 전문 조회할 보유계좌번호</param>
        public void GetOPT50031(string callbackID, string arg1)
        {
            nowCallbackID = callbackID;
            SetInputValue("계좌번호", arg1);
            CommRqData("선옵잔고손익요청", "OPT50031", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50032 기능명:선옵당일실현손익요청</summary>
        ///<param name="arg1">계좌번호 : 전문 조회할 보유계좌번호</param>
        ///<param name="arg2">종목코드 : 전문 조회할 종목코드</param>
        public void GetOPT50032(string callbackID, string arg1, string arg2)
        {
            nowCallbackID = callbackID;
            SetInputValue("계좌번호", arg1);
            SetInputValue("종목코드", arg2);
            CommRqData("선옵당일실현손익요청", "OPT50032", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50033 기능명:선옵잔존일조회요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg2">기준일자 : YYYYMM, 6자리</param>
        public void GetOPT50033(string callbackID, string arg1, string arg2)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            SetInputValue("기준일자", arg2);
            CommRqData("선옵잔존일조회요청", "OPT50033", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50034 기능명:선옵전일가격요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg2">기간 : 5:5일, 10:10일, 20:20일, 60:60일, 250:250일, 250일까지 입력가능</param>
        public void GetOPT50034(string callbackID, string arg1, string arg2)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            SetInputValue("기간", arg2);
            CommRqData("선옵전일가격요청", "OPT50034", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50035 기능명:지수변동성차트요청</summary>
        ///<param name="arg1">종목코드 : 다우존스:_.DJI, 나스닥:_COMP, S&P500:_SPX, 니케이:_JP#NI225, 코스피:001</param>
        ///<param name="arg2">기준일자 : YYYYMM, 6자리</param>
        ///<param name="arg3">기간 : 5:5일, 10:10일, 20:20일, 60:60일, 250:250일, 250일까지 입력가능</param>
        ///<param name="arg4">차트구분 : 일차트:0, 주차트:1, 월차트:2</param>
        public void GetOPT50035(string callbackID, string arg1, string arg2, string arg3, string arg4)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            SetInputValue("기준일자", arg2);
            SetInputValue("기간", arg3);
            SetInputValue("차트구분", arg4);
            CommRqData("지수변동성차트요청", "OPT50035", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50036 기능명:주요지수변동성차트요청</summary>
        ///<param name="arg1">종목코드 : 다우존스:_.DJI, 나스닥:_COMP, S&P500:_SPX, 니케이:_JP#NI225, 코스피:001</param>
        ///<param name="arg2">기준일자 : YYYYMM, 6자리</param>
        ///<param name="arg3">기간 : </param>
        ///<param name="arg4">차트구분 : 일차트:0, 주차트:1, 월차트:2</param>
        public void GetOPT50036(string callbackID, string arg1, string arg2, string arg3, string arg4)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            SetInputValue("기준일자", arg2);
            SetInputValue("기간", arg3);
            SetInputValue("차트구분", arg4);
            CommRqData("주요지수변동성차트요청", "OPT50036", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50037 기능명:코스피200지수요청</summary>
        ///<param name="arg1">종목코드 : 코스피200 지수 선물만 가능</param>
        ///<param name="arg2">기준일자 : YYYYMMDD, 8자리</param>
        public void GetOPT50037(string callbackID, string arg1, string arg2)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            SetInputValue("기준일자", arg2);
            CommRqData("코스피200지수요청", "OPT50037", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50038 기능명:투자자별만기손익차트요청</summary>
        ///<param name="arg1">일자구분 : 일자:1(선택시 반드시 일자입력), 누적:2</param>
        ///<param name="arg2">일자 : YYYYMM, 6자리</param>
        ///<param name="arg3">투자자구분 : 개인:08, 외국인:09, 금융투자:01, 투신:03, 은행:04, 기타금융:05, 보험:02, 연기금등:06, 기타법인:07</param>
        ///<param name="arg4">수량금액구분 : 수량:1, 금액(백만원):2</param>
        public void GetOPT50038(string callbackID, string arg1, string arg2, string arg3, string arg4)
        {
            nowCallbackID = callbackID;
            SetInputValue("일자구분", arg1);
            SetInputValue("일자", arg2);
            SetInputValue("투자자구분", arg3);
            SetInputValue("수량금액구분", arg4);
            CommRqData("투자자별만기손익차트요청", "OPT50038", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50040 기능명:선옵시고저가요청</summary>
        ///<param name="arg1">종목코드 : 코스피200 지수 선물만 가능</param>
        public void GetOPT50040(string callbackID, string arg1)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            CommRqData("선옵시고저가요청", "OPT50040", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50043 기능명:주식선물거래량상위종목요청</summary>
        ///<param name="arg1">거래대금구분 : 거래량상위:0, 거래대금상위(천원):1</param>
        public void GetOPT50043(string callbackID, string arg1)
        {
            nowCallbackID = callbackID;
            SetInputValue("거래대금구분", arg1);
            CommRqData("주식선물거래량상위종목요청", "OPT50043", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50044 기능명:주식선물시세표요청</summary>
        ///<param name="arg1">근월물구분 : 근월물구분</param>
        public void GetOPT50044(string callbackID, string arg1)
        {
            nowCallbackID = callbackID;
            SetInputValue("근월물구분", arg1);
            CommRqData("주식선물시세표요청", "OPT50044", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50062 기능명:선물미결제약정분차트요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg2">시간단위 : 30초:00, 1분:01, 5분:05, 10분:10, 30분:30, 1시간:60</param>
        public void GetOPT50062(string callbackID, string arg1, string arg2)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            SetInputValue("시간단위", arg2);
            CommRqData("선물미결제약정분차트요청", "OPT50062", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50063 기능명:옵션미결제약정일차트요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg2">시간단위 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        public void GetOPT50063(string callbackID, string arg1, string arg2)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            SetInputValue("시간단위", arg2);
            CommRqData("옵션미결제약정일차트요청", "OPT50063", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50064 기능명:옵션미결제약정분차트요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg2">시간단위 : 30초:00, 1분:01, 5분:05, 10분:10, 30분:30, 1시간:60</param>
        public void GetOPT50064(string callbackID, string arg1, string arg2)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            SetInputValue("시간단위", arg2);
            CommRqData("옵션미결제약정분차트요청", "OPT50064", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50065 기능명:풋옵션행사가요청</summary>
        ///<param name="arg1">만기년월 : YYYYMM, 6자리</param>
        public void GetOPT50065(string callbackID, string arg1)
        {
            nowCallbackID = callbackID;
            SetInputValue("만기년월", arg1);
            CommRqData("풋옵션행사가요청", "OPT50065", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50066 기능명:옵션틱차트요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg2">시간단위 : 1틱:1, 3틱:3, 5:5틱:5, 10틱:10, 20틱:20, 30틱:30, 60틱:60, 120틱:120</param>
        public void GetOPT50066(string callbackID, string arg1, string arg2)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            SetInputValue("시간단위", arg2);
            CommRqData("옵션틱차트요청", "OPT50066", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50067 기능명:옵션분차트요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg2">시간단위 : 1분:1, 3분:3, 5분:5, 10분:10, 15분:15, 30분:30, 60분:60, 120분:120</param>
        public void GetOPT50067(string callbackID, string arg1, string arg2)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            SetInputValue("시간단위", arg2);
            CommRqData("옵션분차트요청", "OPT50067", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50068 기능명:옵션일차트요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg2">기준일자 : YYYYMM, 6자리</param>
        public void GetOPT50068(string callbackID, string arg1, string arg2)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            SetInputValue("기준일자", arg2);
            CommRqData("옵션일차트요청", "OPT50068", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50071 기능명:선물주차트요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg2">기준일자 : YYYYMMDD, 8자리</param>
        public void GetOPT50071(string callbackID, string arg1, string arg2)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            SetInputValue("기준일자", arg2);
            CommRqData("선물주차트요청", "OPT50071", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50072 기능명:선물월차트요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg2">기준일자 : YYYYMMDD, 8자리</param>
        public void GetOPT50072(string callbackID, string arg1, string arg2)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            SetInputValue("기준일자", arg2);
            CommRqData("선물월차트요청", "OPT50072", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT50073 기능명:선물년차트요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg2">기준일자 : YYYYMMDD, 8자리</param>
        public void GetOPT50073(string callbackID, string arg1, string arg2)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            SetInputValue("기준일자", arg2);
            CommRqData("선물년차트요청", "OPT50073", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT90001 기능명:테마그룹별요청</summary>
        ///<param name="arg2">검색구분 : 0:전체검색, 1:테마검색, 2:종목검색</param>
        ///<param name="arg3">종목코드 : 검색하려는 종목코드</param>
        ///<param name="arg4">날짜구분 : 1일 ~ 99일 날짜입력</param>
        ///<param name="arg5">테마명 : 검색하려는 테마명</param>
        ///<param name="arg6">등락수익구분 : 1:상위기간수익률, 2:하위기간수익률, 3:상위등락률, 4:하위등락률</param>
        public void GetOPT90001(string callbackID, string arg2, string arg3, string arg4, string arg5, string arg6)
        {
            nowCallbackID = callbackID;
            string screenCode = "0650";
            SetInputValue("화면번호", screenCode);
            SetInputValue("검색구분", arg2);
            SetInputValue("종목코드", arg3);
            SetInputValue("날짜구분", arg4);
            SetInputValue("테마명", arg5);
            SetInputValue("등락수익구분", arg6);
            CommRqData("테마그룹별요청", "OPT90001", 0, screenCode);
        }

        ///<summary> 코드명:OPT90002 기능명:테마구성종목요청</summary>
        ///<param name="arg2">날짜구분 : 1일 ~ 99일 날짜입력</param>
        ///<param name="arg3">종목코드 : 테마그룹코드 번호</param>
        public void GetOPT90002(string callbackID, string arg2, string arg3)
        {
            nowCallbackID = callbackID;
            string screenCode = "0651";
            SetInputValue("화면번호", screenCode);
            SetInputValue("날짜구분", arg2);
            SetInputValue("종목코드", arg3);
            CommRqData("테마구성종목요청", "OPT90002", 0, screenCode);
        }

        ///<summary> 코드명:OPT90003 기능명:프로그램순매수상위50요청</summary>
        ///<param name="arg2">매매상위구분 : 1:순매도상위, 2:순매수상위</param>
        ///<param name="arg3">금액수량구분 : 1:금액, 2:수량</param>
        ///<param name="arg4">시장구분 : P00101:코스피, P10102:코스닥</param>
        public void GetOPT90003(string callbackID, string arg2, string arg3, string arg4)
        {
            nowCallbackID = callbackID;
            string screenCode = "0766";
            SetInputValue("화면번호", screenCode);
            SetInputValue("매매상위구분", arg2);
            SetInputValue("금액수량구분", arg3);
            SetInputValue("시장구분", arg4);
            CommRqData("프로그램순매수상위50요청", "OPT90003", 0, screenCode);
        }

        ///<summary> 코드명:OPT90004 기능명:종목별프로그램매매현황요청</summary>
        ///<param name="arg2">일자 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg3">시장구분 : P00101:코스피, P10102:코스닥</param>
        public void GetOPT90004(string callbackID, string arg2, string arg3)
        {
            nowCallbackID = callbackID;
            string screenCode = "0767";
            SetInputValue("화면번호", screenCode);
            SetInputValue("일자", arg2);
            SetInputValue("시장구분", arg3);
            CommRqData("종목별프로그램매매현황요청", "OPT90004", 0, screenCode);
        }

        ///<summary> 코드명:OPT90005 기능명:프로그램매매추이요청</summary>
        ///<param name="arg2">날짜 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg3">시간구분 : 1:시간대별, 2:일자별</param>
        ///<param name="arg4">금액수량구분 : 1:금액(백만원), 2:수량(천주)</param>
        ///<param name="arg5">시장구분 : P00101:코스피, P10102:코스닥</param>
        ///<param name="arg6">분틱구분 : 0:틱, 1:분</param>
        public void GetOPT90005(string callbackID, string arg2, string arg3, string arg4, string arg5, string arg6)
        {
            nowCallbackID = callbackID;
            string screenCode = "0768";
            SetInputValue("화면번호", screenCode);
            SetInputValue("날짜", arg2);
            SetInputValue("시간구분", arg3);
            SetInputValue("금액수량구분", arg4);
            SetInputValue("시장구분", arg5);
            SetInputValue("분틱구분", arg6);
            CommRqData("프로그램매매추이요청", "OPT90005", 0, screenCode);
        }

        ///<summary> 코드명:OPT90006 기능명:프로그램매매차익잔고추이요청</summary>
        ///<param name="arg2">날짜 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        public void GetOPT90006(string callbackID, string arg2)
        {
            nowCallbackID = callbackID;
            string screenCode = "0773";
            SetInputValue("화면번호", screenCode);
            SetInputValue("날짜", arg2);
            CommRqData("프로그램매매차익잔고추이요청", "OPT90006", 0, screenCode);
        }

        ///<summary> 코드명:OPT90007 기능명:프로그램매매누적추이요청</summary>
        ///<param name="arg2">날짜 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식), 종료일기준 1년간 데이터만 조회가능</param>
        ///<param name="arg3">금액수량구분 : 1:금액, 2:수량</param>
        ///<param name="arg4">시장구분 : P10101:코스피, P10102:코스닥</param>
        public void GetOPT90007(string callbackID, string arg2, string arg3, string arg4)
        {
            nowCallbackID = callbackID;
            string screenCode = "0777";
            SetInputValue("화면번호", screenCode);
            SetInputValue("날짜", arg2);
            SetInputValue("금액수량구분", arg3);
            SetInputValue("시장구분", arg4);
            CommRqData("프로그램매매누적추이요청", "OPT90007", 0, screenCode);
        }

        ///<summary> 코드명:OPT90008 기능명:종목시간별프로그램매매추이요청</summary>
        ///<param name="arg2">시간일자구분 : 1:시간대별</param>
        ///<param name="arg3">금액수량구분 : 1:금액, 2:수량</param>
        ///<param name="arg4">종목코드 : </param>
        ///<param name="arg5">날짜 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        public void GetOPT90008(string callbackID, string arg2, string arg3, string arg4, string arg5)
        {
            nowCallbackID = callbackID;
            string screenCode = "0778";
            SetInputValue("화면번호", screenCode);
            SetInputValue("시간일자구분", arg2);
            SetInputValue("금액수량구분", arg3);
            SetInputValue("종목코드", arg4);
            SetInputValue("날짜", arg5);
            CommRqData("종목시간별프로그램매매추이요청", "OPT90008", 0, screenCode);
        }

        ///<summary> 코드명:OPT90009 기능명:외국인기관매매상위요청</summary>
        ///<param name="arg2">시장구분 : 000:전체, 001:코스피, 101:코스닥</param>
        ///<param name="arg3">금액수량구분 : 1:금액(천만), 2:수량(천)</param>
        ///<param name="arg4">조회일자구분 : 0:조회일자 미포함, 1:조회일자 포함</param>
        ///<param name="arg5">날짜 : YYYYMMDD (20160101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        public void GetOPT90009(string callbackID, string arg2, string arg3, string arg4, string arg5)
        {
            nowCallbackID = callbackID;
            string screenCode = "0785";
            SetInputValue("화면번호", screenCode);
            SetInputValue("시장구분", arg2);
            SetInputValue("금액수량구분", arg3);
            SetInputValue("조회일자구분", arg4);
            SetInputValue("날짜", arg5);
            CommRqData("외국인기관매매상위요청", "OPT90009", 0, screenCode);
        }

        ///<summary> 코드명:OPT90010 기능명:차익잔고현황요청</summary>
        ///<param name="arg2">일자 : YYYYMMDD (20170101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg3">금액수량구분 : 1:금액, 2:수량</param>
        public void GetOPT90010(string callbackID, string arg2, string arg3)
        {
            nowCallbackID = callbackID;
            string screenCode = "0774";
            SetInputValue("화면번호", screenCode);
            SetInputValue("일자", arg2);
            SetInputValue("금액수량구분", arg3);
            CommRqData("차익잔고현황요청", "OPT90010", 0, screenCode);
        }

        ///<summary> 코드명:OPT90011 기능명:차익잔고현황요청</summary>
        ///<param name="arg1">일자 : YYYYMMDD (20170101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg2">금액수량구분 : 1:금액, 2:수량</param>
        public void GetOPT90011(string callbackID, string arg1, string arg2)
        {
            nowCallbackID = callbackID;
            SetInputValue("일자", arg1);
            SetInputValue("금액수량구분", arg2);
            CommRqData("차익잔고현황요청", "OPT90011", 0, GetScrNum());
        }

        ///<summary> 코드명:OPT90012 기능명:대차거래내역요청</summary>
        ///<param name="arg2">일자 : YYYYMMDD (20170101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg3">시장구분 : 001:코스피, 101:코스닥</param>
        public void GetOPT90012(string callbackID, string arg2, string arg3)
        {
            nowCallbackID = callbackID;
            string screenCode = "1060";
            SetInputValue("화면번호", screenCode);
            SetInputValue("일자", arg2);
            SetInputValue("시장구분", arg3);
            CommRqData("대차거래내역요청", "OPT90012", 0, screenCode);
        }

        ///<summary> 코드명:OPT90013 기능명:종목일별프로그램매매추이요청</summary>
        ///<param name="arg2">시간일자구분 : 2:일자별</param>
        ///<param name="arg3">금액수량구분 : 1:금액, 2:수량</param>
        ///<param name="arg4">종목코드 : </param>
        ///<param name="arg5">날짜 : YYYYMMDD (20170101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        public void GetOPT90013(string callbackID, string arg2, string arg3, string arg4, string arg5)
        {
            nowCallbackID = callbackID;
            string screenCode = "0778";
            SetInputValue("화면번호", screenCode);
            SetInputValue("시간일자구분", arg2);
            SetInputValue("금액수량구분", arg3);
            SetInputValue("종목코드", arg4);
            SetInputValue("날짜", arg5);
            CommRqData("종목일별프로그램매매추이요청", "OPT90013", 0, screenCode);
        }

        ///<summary> 코드명:OPT99999 기능명:대차거래상위10종목요청</summary>
        ///<param name="arg1">시작일자 : YYYYMMDD (20170101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg2">종료일자 : YYYYMMDD (20170101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg3">시장구분 : 0:전체, 1:장내, 2:코스닥, 3:OTCBB, 4:ECN</param>
        public void GetOPT99999(string callbackID, string arg1, string arg2, string arg3)
        {
            nowCallbackID = callbackID;
            SetInputValue("시작일자", arg1);
            SetInputValue("종료일자", arg2);
            SetInputValue("시장구분", arg3);
            CommRqData("대차거래상위10종목요청", "OPT99999", 0, GetScrNum());
        }

        ///<summary> 코드명:OPTFOFID 기능명:선물전체시세요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        public void GetOPTFOFID(string callbackID, string arg1)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            CommRqData("선물전체시세요청", "OPTFOFID", 0, GetScrNum());
        }

        ///<summary> 코드명:OPTKWFID 기능명:관심종목정보요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        public void GetOPTKWFID(string callbackID, string arg1)
        {
            nowCallbackID = callbackID;
            //SetInputValue("종목코드", arg1);
            //CommRqData("관심종목정보요청", "OPTKWFID", 0, GetScrNum());
            CommKwRqData(arg1, GetScrNum());
        }

        ///<summary> 코드명:OPTKWINV 기능명:관심종목투자자정보요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        public void GetOPTKWINV(string callbackID, string arg1)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            CommRqData("관심종목투자자정보요청", "OPTKWINV", 0, GetScrNum());
        }

        ///<summary> 코드명:OPTKWPRO 기능명:관심종목프로그램정보요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        public void GetOPTKWPRO(string callbackID, string arg1)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            CommRqData("관심종목프로그램정보요청", "OPTKWPRO", 0, GetScrNum());
        }

        ///<summary> 코드명:OPW00001 기능명:예수금상세현황요청</summary>
        ///<param name="arg2">계좌번호 : 전문 조회할 보유계좌번호</param>
        ///<param name="arg3">비밀번호 : 사용안함(공백)</param>
        ///<param name="arg4">비밀번호입력매체구분 : 00</param>
        ///<param name="arg5">조회구분 : 1:추정조회, 2:일반조회</param>
        public void GetOPW00001(string callbackID, string arg2, string arg3, string arg4, string arg5)
        {
            nowCallbackID = callbackID;
            string screenCode = "0362";
            SetInputValue("화면번호", screenCode);
            SetInputValue("계좌번호", arg2);
            SetInputValue("비밀번호", arg3);
            SetInputValue("비밀번호입력매체구분", arg4);
            SetInputValue("조회구분", arg5);
            CommRqData("예수금상세현황요청", "OPW00001", 0, screenCode);
        }

        ///<summary> 코드명:OPW00002 기능명:일별추정예탁자산현황요청</summary>
        ///<param name="arg2">계좌번호 : 전문 조회할 보유계좌번호</param>
        ///<param name="arg3">비밀번호 : 사용안함(공백)</param>
        ///<param name="arg4">시작조회기간 : YYYYMMDD (20170101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg5">종료조회기간 : YYYYMMDD (20170101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        public void GetOPW00002(string callbackID, string arg2, string arg3, string arg4, string arg5)
        {
            nowCallbackID = callbackID;
            string screenCode = "0349";
            SetInputValue("화면번호", screenCode);
            SetInputValue("계좌번호", arg2);
            SetInputValue("비밀번호", arg3);
            SetInputValue("시작조회기간", arg4);
            SetInputValue("종료조회기간", arg5);
            CommRqData("일별추정예탁자산현황요청", "OPW00002", 0, screenCode);
        }

        ///<summary> 코드명:OPW00003 기능명:추정자산조회요청</summary>
        ///<param name="arg1">계좌번호 : 전문 조회할 보유계좌번호</param>
        ///<param name="arg2">비밀번호 : 사용안함(공백)</param>
        ///<param name="arg3">상장폐지조회구분 : 0:전체, 1:상장폐지종목제외</param>
        public void GetOPW00003(string callbackID, string arg1, string arg2, string arg3)
        {
            nowCallbackID = callbackID;
            SetInputValue("계좌번호", arg1);
            SetInputValue("비밀번호", arg2);
            SetInputValue("상장폐지조회구분", arg3);
            CommRqData("추정자산조회요청", "OPW00003", 0, GetScrNum());
        }

        ///<summary> 코드명:OPW00004 기능명:계좌평가현황요청</summary>
        ///<param name="arg1">계좌번호 : 전문 조회할 보유계좌번호</param>
        ///<param name="arg2">비밀번호 : 사용안함(공백)</param>
        ///<param name="arg3">상장폐지조회구분 : 0:전체, 1:상장폐지종목제외</param>
        ///<param name="arg4">비밀번호입력매체구분 : 00</param>
        public void GetOPW00004(string callbackID, string arg1, string arg2, string arg3, string arg4)
        {
            nowCallbackID = callbackID;
            SetInputValue("계좌번호", arg1);
            SetInputValue("비밀번호", arg2);
            SetInputValue("상장폐지조회구분", arg3);
            SetInputValue("비밀번호입력매체구분", arg4);
            CommRqData("계좌평가현황요청", "OPW00004", 0, GetScrNum());
        }

        ///<summary> 코드명:OPW00005 기능명:체결잔고요청</summary>
        ///<param name="arg1">계좌번호 : 전문 조회할 보유계좌번호</param>
        ///<param name="arg2">비밀번호 : 사용안함(공백)</param>
        ///<param name="arg3">비밀번호입력매체구분 : 00</param>
        public void GetOPW00005(string callbackID, string arg1, string arg2, string arg3)
        {
            nowCallbackID = callbackID;
            SetInputValue("계좌번호", arg1);
            SetInputValue("비밀번호", arg2);
            SetInputValue("비밀번호입력매체구분", arg3);
            CommRqData("체결잔고요청", "OPW00005", 0, GetScrNum());
        }

        ///<summary> 코드명:OPW00006 기능명:관리자별주문체결내역요청</summary>
        ///<param name="arg1">일자 : YYYYMMDD (20170101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg2">지점코드 : 0</param>
        ///<param name="arg3">시작주문번호 : 0</param>
        public void GetOPW00006(string callbackID, string arg1, string arg2, string arg3)
        {
            nowCallbackID = callbackID;
            SetInputValue("일자", arg1);
            SetInputValue("지점코드", arg2);
            SetInputValue("시작주문번호", arg3);
            CommRqData("관리자별주문체결내역요청", "OPW00006", 0, GetScrNum());
        }

        ///<summary> 코드명:OPW00007 기능명:계좌별주문체결내역상세요청</summary>
        ///<param name="arg2">주문일자 : YYYYMMDD (20170101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg3">계좌번호 : 전문 조회할 보유계좌번호</param>
        ///<param name="arg4">비밀번호 : 사용안함(공백)</param>
        ///<param name="arg5">비밀번호입력매체구분 : 00</param>
        ///<param name="arg6">조회구분 : 1:주문순, 2:역순, 3:미체결, 4:체결내역만</param>
        ///<param name="arg7">주식채권구분 : 0:전체, 1:주식, 2:채권</param>
        ///<param name="arg8">매도수구분 : 0:전체, 1:매도, 2:매수</param>
        ///<param name="arg9">종목코드 : </param>
        ///<param name="arg10">시작주문번호 : </param>
        public void GetOPW00007(string callbackID, string arg2, string arg3, string arg4, string arg5, string arg6, string arg7, string arg8, string arg9, string arg10)
        {
            nowCallbackID = callbackID;
            string screenCode = "0351";
            SetInputValue("화면번호", screenCode);
            SetInputValue("주문일자", arg2);
            SetInputValue("계좌번호", arg3);
            SetInputValue("비밀번호", arg4);
            SetInputValue("비밀번호입력매체구분", arg5);
            SetInputValue("조회구분", arg6);
            SetInputValue("주식채권구분", arg7);
            SetInputValue("매도수구분", arg8);
            SetInputValue("종목코드", arg9);
            SetInputValue("시작주문번호", arg10);
            CommRqData("계좌별주문체결내역상세요청", "OPW00007", 0, screenCode);
        }

        ///<summary> 코드명:OPW00008 기능명:계좌별익일결제예정내역요청</summary>
        ///<param name="arg1">계좌번호 : 전문 조회할 보유계좌번호</param>
        ///<param name="arg2">비밀번호 : 사용안함(공백)</param>
        ///<param name="arg3">비밀번호입력매체구분 : 00</param>
        ///<param name="arg4">시작결제번호 : </param>
        public void GetOPW00008(string callbackID, string arg1, string arg2, string arg3, string arg4)
        {
            nowCallbackID = callbackID;
            SetInputValue("계좌번호", arg1);
            SetInputValue("비밀번호", arg2);
            SetInputValue("비밀번호입력매체구분", arg3);
            SetInputValue("시작결제번호", arg4);
            CommRqData("계좌별익일결제예정내역요청", "OPW00008", 0, GetScrNum());
        }

        ///<summary> 코드명:OPW00009 기능명:계좌별주문체결현황요청</summary>
        ///<param name="arg2">주문일자 : YYYYMMDD (20170101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg3">계좌번호 : 전문 조회할 보유계좌번호</param>
        ///<param name="arg4">비밀번호 : 사용안함(공백)</param>
        ///<param name="arg5">비밀번호입력매체구분 : 00</param>
        ///<param name="arg6">주식채권구분 : 0:전체, 1:주식, 2:채권</param>
        ///<param name="arg7">시장구분 : 0:전체, 1:장내, 2:코스닥, 3:OTCBB, 4:ECN</param>
        ///<param name="arg8">매도수구분 : 0:전체, 1:매도, 2:매수</param>
        ///<param name="arg9">조회구분 : 0:전체, 1:체결</param>
        ///<param name="arg10">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg11">시작주문번호 : </param>
        public void GetOPW00009(string callbackID, string arg2, string arg3, string arg4, string arg5, string arg6, string arg7, string arg8, string arg9, string arg10, string arg11)
        {
            nowCallbackID = callbackID;
            string screenCode = "0343";
            SetInputValue("화면번호", screenCode);
            SetInputValue("주문일자", arg2);
            SetInputValue("계좌번호", arg3);
            SetInputValue("비밀번호", arg4);
            SetInputValue("비밀번호입력매체구분", arg5);
            SetInputValue("주식채권구분", arg6);
            SetInputValue("시장구분", arg7);
            SetInputValue("매도수구분", arg8);
            SetInputValue("조회구분", arg9);
            SetInputValue("종목코드", arg10);
            SetInputValue("시작주문번호", arg11);
            CommRqData("계좌별주문체결현황요청", "OPW00009", 0, screenCode);
        }

        ///<summary> 코드명:OPW00010 기능명:주문인출가능금액요청</summary>
        ///<param name="arg2">계좌번호 : 전문 조회할 보유계좌번호</param>
        ///<param name="arg3">비밀번호 : 사용안함(공백)</param>
        ///<param name="arg4">비밀번호입력매체구분 : 00</param>
        ///<param name="arg5">입출금액 : </param>
        ///<param name="arg6">종목번호 : </param>
        ///<param name="arg7">매매구분 : 1:매도, 2:매수</param>
        ///<param name="arg8">매매수량 : </param>
        ///<param name="arg9">매수가격 : </param>
        ///<param name="arg10">예상매수단가 : </param>
        public void GetOPW00010(string callbackID, string arg2, string arg3, string arg4, string arg5, string arg6, string arg7, string arg8, string arg9, string arg10)
        {
            nowCallbackID = callbackID;
            string screenCode = "0347";
            SetInputValue("화면번호", screenCode);
            SetInputValue("계좌번호", arg2);
            SetInputValue("비밀번호", arg3);
            SetInputValue("비밀번호입력매체구분", arg4);
            SetInputValue("입출금액", arg5);
            SetInputValue("종목번호", arg6);
            SetInputValue("매매구분", arg7);
            SetInputValue("매매수량", arg8);
            SetInputValue("매수가격", arg9);
            SetInputValue("예상매수단가", arg10);
            CommRqData("주문인출가능금액요청", "OPW00010", 0, screenCode);
        }

        ///<summary> 코드명:OPW00011 기능명:증거금율별주문가능수량조회요청</summary>
        ///<param name="arg2">계좌번호 : 전문 조회할 보유계좌번호</param>
        ///<param name="arg3">비밀번호 : 사용안함(공백)</param>
        ///<param name="arg4">비밀번호입력매체구분 : 00</param>
        ///<param name="arg5">종목번호 : </param>
        ///<param name="arg6">매수가격 : </param>
        public void GetOPW00011(string callbackID, string arg2, string arg3, string arg4, string arg5, string arg6)
        {
            nowCallbackID = callbackID;
            string screenCode = "0347";
            SetInputValue("화면번호", screenCode);
            SetInputValue("계좌번호", arg2);
            SetInputValue("비밀번호", arg3);
            SetInputValue("비밀번호입력매체구분", arg4);
            SetInputValue("종목번호", arg5);
            SetInputValue("매수가격", arg6);
            CommRqData("증거금율별주문가능수량조회요청", "OPW00011", 0, screenCode);
        }

        ///<summary> 코드명:OPW00012 기능명:신용보증금율별주문가능수량조회요청</summary>
        ///<param name="arg1">계좌번호 : 전문 조회할 보유계좌번호</param>
        ///<param name="arg2">비밀번호 : 사용안함(공백)</param>
        ///<param name="arg3">비밀번호입력매체구분 : 00</param>
        ///<param name="arg4">종목번호 : </param>
        ///<param name="arg5">매수가격 : </param>
        public void GetOPW00012(string callbackID, string arg1, string arg2, string arg3, string arg4, string arg5)
        {
            nowCallbackID = callbackID;
            SetInputValue("계좌번호", arg1);
            SetInputValue("비밀번호", arg2);
            SetInputValue("비밀번호입력매체구분", arg3);
            SetInputValue("종목번호", arg4);
            SetInputValue("매수가격", arg5);
            CommRqData("신용보증금율별주문가능수량조회요청", "OPW00012", 0, GetScrNum());
        }

        ///<summary> 코드명:OPW00013 기능명:증거금세부내역조회요청</summary>
        ///<param name="arg1">계좌번호 : 전문 조회할 보유계좌번호</param>
        ///<param name="arg2">비밀번호 : 사용안함(공백)</param>
        public void GetOPW00013(string callbackID, string arg1, string arg2)
        {
            nowCallbackID = callbackID;
            SetInputValue("계좌번호", arg1);
            SetInputValue("비밀번호", arg2);
            CommRqData("증거금세부내역조회요청", "OPW00013", 0, GetScrNum());
        }

        ///<summary> 코드명:OPW00014 기능명:비밀번호일치여부요청</summary>
        ///<param name="arg1">계좌번호 : 전문 조회할 보유계좌번호</param>
        ///<param name="arg2">비밀번호 : 사용안함(공백)</param>
        ///<param name="arg3">비밀번호입력매체구분 : 00</param>
        public void GetOPW00014(string callbackID, string arg1, string arg2, string arg3)
        {
            nowCallbackID = callbackID;
            SetInputValue("계좌번호", arg1);
            SetInputValue("비밀번호", arg2);
            SetInputValue("비밀번호입력매체구분", arg3);
            CommRqData("비밀번호일치여부요청", "OPW00014", 0, GetScrNum());
        }

        ///<summary> 코드명:OPW00015 기능명:위탁종합거래내역요청</summary>
        ///<param name="arg2">계좌번호 : 전문 조회할 보유계좌번호</param>
        ///<param name="arg3">비밀번호 : 사용안함(공백)</param>
        ///<param name="arg4">시작일자 : YYYYMMDD (20170101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg5">종료일자 : YYYYMMDD (20170101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg6">구분 : 0:전체, 1:입출금, 2:입출고, 3:매매, 4:매수, 5:매도, 6:입금, 7:출금, A:예탁담보대출입금, F:환전</param>
        ///<param name="arg7">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg8">통화코드 : 공백:전체, "CNY", "EUR", "HKD", "JPY", "USD"</param>
        ///<param name="arg9">상품구분 : 1, 0:전체, 1:국내주식, 2:수익증권, 3:해외주식, 4:금융상품</param>
        ///<param name="arg10">비밀번호입력매체구분 : 00</param>
        ///<param name="arg11">고객정보제한여부 : Y:제한,N:비제한</param>
        public void GetOPW00015(string callbackID, string arg2, string arg3, string arg4, string arg5, string arg6, string arg7, string arg8, string arg9, string arg10, string arg11)
        {
            nowCallbackID = callbackID;
            string screenCode = "0399";
            SetInputValue("화면번호", screenCode);
            SetInputValue("계좌번호", arg2);
            SetInputValue("비밀번호", arg3);
            SetInputValue("시작일자", arg4);
            SetInputValue("종료일자", arg5);
            SetInputValue("구분", arg6);
            SetInputValue("종목코드", arg7);
            SetInputValue("통화코드", arg8);
            SetInputValue("상품구분", arg9);
            SetInputValue("비밀번호입력매체구분", arg10);
            SetInputValue("고객정보제한여부", arg11);
            CommRqData("위탁종합거래내역요청", "OPW00015", 0, screenCode);
        }

        ///<summary> 코드명:OPW00016 기능명:일별계좌수익률상세현황요청</summary>
        ///<param name="arg1">계좌번호 : 전문 조회할 보유계좌번호</param>
        ///<param name="arg2">비밀번호 : 사용안함(공백)</param>
        ///<param name="arg3">평가시작일 : </param>
        ///<param name="arg4">평가종료일 : </param>
        ///<param name="arg5">비밀번호입력매체구분 : 00</param>
        public void GetOPW00016(string callbackID, string arg1, string arg2, string arg3, string arg4, string arg5)
        {
            nowCallbackID = callbackID;
            SetInputValue("계좌번호", arg1);
            SetInputValue("비밀번호", arg2);
            SetInputValue("평가시작일", arg3);
            SetInputValue("평가종료일", arg4);
            SetInputValue("비밀번호입력매체구분", arg5);
            CommRqData("일별계좌수익률상세현황요청", "OPW00016", 0, GetScrNum());
        }

        ///<summary> 코드명:OPW00017 기능명:계좌별당일현황요청</summary>
        ///<param name="arg1">계좌번호 : 전문 조회할 보유계좌번호</param>
        ///<param name="arg2">비밀번호 : 사용안함(공백)</param>
        ///<param name="arg3">비밀번호입력매체구분 : 00</param>
        public void GetOPW00017(string callbackID, string arg1, string arg2, string arg3)
        {
            nowCallbackID = callbackID;
            SetInputValue("계좌번호", arg1);
            SetInputValue("비밀번호", arg2);
            SetInputValue("비밀번호입력매체구분", arg3);
            CommRqData("계좌별당일현황요청", "OPW00017", 0, GetScrNum());
        }

        ///<summary> 코드명:OPW00018 기능명:계좌평가잔고내역요청</summary>
        ///<param name="arg1">계좌번호 : 전문 조회할 보유계좌번호</param>
        ///<param name="arg2">비밀번호 : 사용안함(공백)</param>
        ///<param name="arg3">비밀번호입력매체구분 : 00</param>
        ///<param name="arg4">조회구분 : 1:합산, 2:개별</param>
        public void GetOPW00018(string callbackID, string arg1, string arg2, string arg3, string arg4)
        {
            nowCallbackID = callbackID;
            SetInputValue("계좌번호", arg1);
            SetInputValue("비밀번호", arg2);
            SetInputValue("비밀번호입력매체구분", arg3);
            SetInputValue("조회구분", arg4);
            CommRqData("계좌평가잔고내역요청", "OPW00018", 0, GetScrNum());
        }

        ///<summary> 코드명:OPW10001 기능명:ELW종목별민감도지표요청</summary>
        ///<param name="arg1">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg2">연속구분 : </param>
        ///<param name="arg3">연속키 : </param>
        public void GetOPW10001(string callbackID, string arg1, string arg2, string arg3)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목코드", arg1); 
            SetInputValue("연속구분", arg2);
            SetInputValue("연속키", arg3);
            CommRqData("ELW종목별민감도지표요청", "OPW10001", 0, GetScrNum());
        }

        ///<summary> 코드명:OPW10002 기능명:ELW투자지표요청</summary>
        ///<param name="arg1">연속구분 : </param>
        ///<param name="arg2">연속키 : </param>
        ///<param name="arg3">종목코드 : 전문 조회할 종목코드</param>
        public void GetOPW10002(string callbackID, string arg1, string arg2, string arg3)
        {
            nowCallbackID = callbackID;
            SetInputValue("연속구분", arg1);
            SetInputValue("연속키", arg2);
            SetInputValue("종목코드", arg3);
            CommRqData("ELW투자지표요청", "OPW10002", 0, GetScrNum());
        }

        ///<summary> 코드명:OPW10003 기능명:ELW민감도지표요청</summary>
        ///<param name="arg1">연속구분 : 9999</param>
        ///<param name="arg2">연속키 : 1212</param>
        ///<param name="arg3">종목코드 : 전문 조회할 종목코드</param>
        public void GetOPW10003(string callbackID, string arg1, string arg2, string arg3)
        {
            nowCallbackID = callbackID;
            SetInputValue("연속구분", arg1);
            SetInputValue("연속키", arg2);
            SetInputValue("종목코드", arg3);
            CommRqData("ELW민감도지표요청", "OPW10003", 0, GetScrNum());
        }

        ///<summary> 코드명:OPW10004 기능명:업종별순매수요청</summary>
        ///<param name="arg1">업종구분 : 9292</param>
        ///<param name="arg2">조회조건 : 9999</param>
        public void GetOPW10004(string callbackID, string arg1, string arg2)
        {
            nowCallbackID = callbackID;
            SetInputValue("업종구분", arg1);
            SetInputValue("조회조건", arg2);
            CommRqData("업종별순매수요청", "OPW10004", 0, GetScrNum());
        }

        ///<summary> 코드명:OPW20001 기능명:선물옵션청산주문위탁증거금가계산요청</summary>
        ///<param name="arg1">계좌번호 : 전문 조회할 보유계좌번호</param>
        ///<param name="arg2">비밀번호 : 사용안함(공백)</param>
        ///<param name="arg3">입력건수 : </param>
        ///<param name="arg4">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg5">매수매도구분 : 1:매도, 2:매수</param>
        ///<param name="arg6">주문수량 : </param>
        ///<param name="arg7">잔고수량 : </param>
        public void GetOPW20001(string callbackID, string arg1, string arg2, string arg3, string arg4, string arg5, string arg6, string arg7)
        {
            nowCallbackID = callbackID;
            SetInputValue("계좌번호", arg1);
            SetInputValue("비밀번호", arg2);
            SetInputValue("입력건수", arg3);
            SetInputValue("종목코드", arg4);
            SetInputValue("매수매도구분", arg5);
            SetInputValue("주문수량", arg6);
            SetInputValue("잔고수량", arg7);
            CommRqData("선물옵션청산주문위탁증거금가계산요청", "OPW20001", 0, GetScrNum());
        }

        ///<summary> 코드명:OPW20002 기능명:선옵당일매매변동현황요청</summary>
        ///<param name="arg1">계좌번호 : 전문 조회할 보유계좌번호</param>
        ///<param name="arg2">비밀번호 : 사용안함(공백)</param>
        ///<param name="arg3">시장구분 : 1:KOSPI지수</param>
        ///<param name="arg4">체결일자 : YYYMMDD (20170101 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg5">비밀번호입력매체구분 : 00</param>
        public void GetOPW20002(string callbackID, string arg1, string arg2, string arg3, string arg4, string arg5)
        {
            nowCallbackID = callbackID;
            SetInputValue("계좌번호", arg1);
            SetInputValue("비밀번호", arg2);
            SetInputValue("시장구분", arg3);
            SetInputValue("체결일자", arg4);
            SetInputValue("비밀번호입력매체구분", arg5);
            CommRqData("선옵당일매매변동현황요청", "OPW20002", 0, GetScrNum());
        }

        ///<summary> 코드명:OPW20003 기능명:선옵기간손익조회요청</summary>
        ///<param name="arg1">계좌번호 : 전문 조회할 보유계좌번호</param>
        ///<param name="arg2">시장구분 : 0:전체</param>
        ///<param name="arg3">비밀번호 : 사용안함(공백)</param>
        ///<param name="arg4">시작일자 : YYYYMMDD (연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg5">종료일자 : YYYYMMDD (연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg6">비밀번호입력매체구분 : 00</param>
        public void GetOPW20003(string callbackID, string arg1, string arg2, string arg3, string arg4, string arg5, string arg6)
        {
            nowCallbackID = callbackID;
            SetInputValue("계좌번호", arg1);
            SetInputValue("시장구분", arg2);
            SetInputValue("비밀번호", arg3);
            SetInputValue("시작일자", arg4);
            SetInputValue("종료일자", arg5);
            SetInputValue("비밀번호입력매체구분", arg6);
            CommRqData("선옵기간손익조회요청", "OPW20003", 0, GetScrNum());
        }

        ///<summary> 코드명:OPW20004 기능명:선옵주문체결내역상세요청</summary>
        ///<param name="arg1">계좌번호 : 전문 조회할 보유계좌번호</param>
        ///<param name="arg2">비밀번호 : 사용안함(공백)</param>
        ///<param name="arg3">조회일 : 조회일 입력(YYYYMMDD 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg4">종목구분 : 0:전체, 1:코스피선물, 2:코스피옵션</param>
        ///<param name="arg5">조회구분 : 0:전체, 1:체결, 2:미체결</param>
        ///<param name="arg6">정렬구분 : 1:주문번호순,  2:주문번호역순</param>
        ///<param name="arg7">비밀번호입력매체구분 : 00</param>
        ///<param name="arg8">정규시간외구분 : 0:전체, 1:정규장</param>
        public void GetOPW20004(string callbackID, string arg1, string arg2, string arg3, string arg4, string arg5, string arg6, string arg7, string arg8)
        {
            nowCallbackID = callbackID;
            SetInputValue("계좌번호", arg1);
            SetInputValue("비밀번호", arg2);
            SetInputValue("조회일", arg3);
            SetInputValue("종목구분", arg4);
            SetInputValue("조회구분", arg5);
            SetInputValue("정렬구분", arg6);
            SetInputValue("비밀번호입력매체구분", arg7);
            SetInputValue("정규시간외구분", arg8);
            CommRqData("선옵주문체결내역상세요청", "OPW20004", 0, GetScrNum());
        }

        ///<summary> 코드명:OPW20005 기능명:선옵주문체결내역상세평균가요청</summary>
        ///<param name="arg1">계좌번호 : 전문 조회할 보유계좌번호</param>
        ///<param name="arg2">비밀번호 : 사용안함(공백)</param>
        ///<param name="arg3">조회일 : 조회일 입력(YYYYMMDD 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg4">종목구분 : 0:전체, 1:코스피선물, 2:코스피옵션</param>
        ///<param name="arg5">조회구분 : 0:전체, 1:체결, 2:미체결</param>
        ///<param name="arg6">정렬구분 : 1:주문번호순,  2:주문번호역순</param>
        ///<param name="arg7">정규시간외구분 : 0:전체, 1:정규장</param>
        ///<param name="arg8">비밀번호입력매체구분 : 00</param>
        public void GetOPW20005(string callbackID, string arg1, string arg2, string arg3, string arg4, string arg5, string arg6, string arg7, string arg8)
        {
            nowCallbackID = callbackID;
            SetInputValue("계좌번호", arg1);
            SetInputValue("비밀번호", arg2);
            SetInputValue("조회일", arg3);
            SetInputValue("종목구분", arg4);
            SetInputValue("조회구분", arg5);
            SetInputValue("정렬구분", arg6);
            SetInputValue("정규시간외구분", arg7);
            SetInputValue("비밀번호입력매체구분", arg8);
            CommRqData("선옵주문체결내역상세평균가요청", "OPW20005", 0, GetScrNum());
        }

        ///<summary> 코드명:OPW20006 기능명:선옵잔고상세현황요청</summary>
        ///<param name="arg1">계좌번호 : 전문 조회할 보유계좌번호</param>
        ///<param name="arg2">비밀번호 : 사용안함(공백)</param>
        ///<param name="arg3">조회일자 : 조회일 입력(YYYYMMDD 연도4자리, 월 2자리, 일 2자리 형식)</param>
        ///<param name="arg4">비밀번호입력매체구분 : 00</param>
        public void GetOPW20006(string callbackID, string arg1, string arg2, string arg3, string arg4)
        {
            nowCallbackID = callbackID;
            SetInputValue("계좌번호", arg1);
            SetInputValue("비밀번호", arg2);
            SetInputValue("조회일자", arg3);
            SetInputValue("비밀번호입력매체구분", arg4);
            CommRqData("선옵잔고상세현황요청", "OPW20006", 0, GetScrNum());
        }

        ///<summary> 코드명:OPW20007 기능명:선옵잔고현황정산가기준요청</summary>
        ///<param name="arg1">계좌번호 : 전문 조회할 보유계좌번호</param>
        ///<param name="arg2">비밀번호 : 사용안함(공백)</param>
        ///<param name="arg3">비밀번호입력매체구분 : 00</param>
        public void GetOPW20007(string callbackID, string arg1, string arg2, string arg3)
        {
            nowCallbackID = callbackID;
            SetInputValue("계좌번호", arg1);
            SetInputValue("비밀번호", arg2);
            SetInputValue("비밀번호입력매체구분", arg3);
            CommRqData("선옵잔고현황정산가기준요청", "OPW20007", 0, GetScrNum());
        }

        ///<summary> 코드명:OPW20008 기능명:계좌별결제예상내역조회요청</summary>
        ///<param name="arg1">계좌번호 : 전문 조회할 보유계좌번호</param>
        ///<param name="arg2">비밀번호 : 사용안함(공백)</param>
        ///<param name="arg3">비밀번호입력매체구분 : 00</param>
        public void GetOPW20008(string callbackID, string arg1, string arg2, string arg3)
        {
            nowCallbackID = callbackID;
            SetInputValue("계좌번호", arg1);
            SetInputValue("비밀번호", arg2);
            SetInputValue("비밀번호입력매체구분", arg3);
            CommRqData("계좌별결제예상내역조회요청", "OPW20008", 0, GetScrNum());
        }

        ///<summary> 코드명:OPW20009 기능명:선옵계좌별주문가능수량요청</summary>
        ///<param name="arg1">계좌번호 : 전문 조회할 보유계좌번호</param>
        ///<param name="arg2">비밀번호 : 사용안함(공백)</param>
        ///<param name="arg3">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg4">매도수구분 : 1:매도, 2:매수</param>
        ///<param name="arg5">주문유형 : 1:지정가</param>
        ///<param name="arg6">주문가격 : 소수점제거한값(123.45 -> 12345)</param>
        ///<param name="arg7">비밀번호입력매체구분 : 00</param>
        public void GetOPW20009(string callbackID, string arg1, string arg2, string arg3, string arg4, string arg5, string arg6, string arg7)
        {
            nowCallbackID = callbackID;
            SetInputValue("계좌번호", arg1);
            SetInputValue("비밀번호", arg2);
            SetInputValue("종목코드", arg3);
            SetInputValue("매도수구분", arg4);
            SetInputValue("주문유형", arg5);
            SetInputValue("주문가격", arg6);
            SetInputValue("비밀번호입력매체구분", arg7);
            CommRqData("선옵계좌별주문가능수량요청", "OPW20009", 0, GetScrNum());
        }

        ///<summary> 코드명:OPW20010 기능명:선옵예탁금및증거금조회요청</summary>
        ///<param name="arg1">계좌번호 : 전문 조회할 보유계좌번호</param>
        ///<param name="arg2">비밀번호 : 사용안함(공백)</param>
        ///<param name="arg3">비밀번호입력매체구분 : 00</param>
        public void GetOPW20010(string callbackID, string arg1, string arg2, string arg3)
        {
            nowCallbackID = callbackID;
            SetInputValue("계좌번호", arg1);
            SetInputValue("비밀번호", arg2);
            SetInputValue("비밀번호입력매체구분", arg3);
            CommRqData("선옵예탁금및증거금조회요청", "OPW20010", 0, GetScrNum());
        }

        ///<summary> 코드명:OPW20011 기능명:선옵계좌예비증거금상세요청</summary>
        ///<param name="arg1">계좌번호 : 전문 조회할 보유계좌번호</param>
        ///<param name="arg2">비밀번호 : 사용안함(공백)</param>
        ///<param name="arg3">증거금구분 : 0:위탁산출용, 1:유지산출용</param>
        ///<param name="arg4">비밀번호입력매체구분 : 00</param>
        public void GetOPW20011(string callbackID, string arg1, string arg2, string arg3, string arg4)
        {
            nowCallbackID = callbackID;
            SetInputValue("계좌번호", arg1);
            SetInputValue("비밀번호", arg2);
            SetInputValue("증거금구분", arg3);
            SetInputValue("비밀번호입력매체구분", arg4);
            CommRqData("선옵계좌예비증거금상세요청", "OPW20011", 0, GetScrNum());
        }

        ///<summary> 코드명:OPW20012 기능명:선옵증거금상세내역요청</summary>
        ///<param name="arg1">계좌번호 : 전문 조회할 보유계좌번호</param>
        ///<param name="arg2">비밀번호 : 사용안함(공백)</param>
        ///<param name="arg3">비밀번호입력매체구분 : 00</param>
        public void GetOPW20012(string callbackID, string arg1, string arg2, string arg3)
        {
            nowCallbackID = callbackID;
            SetInputValue("계좌번호", arg1);
            SetInputValue("비밀번호", arg2);
            SetInputValue("비밀번호입력매체구분", arg3);
            CommRqData("선옵증거금상세내역요청", "OPW20012", 0, GetScrNum());
        }

        ///<summary> 코드명:OPW20013 기능명:계좌미결제청산가능수량조회요청</summary>
        ///<param name="arg1">계좌번호 : 전문 조회할 보유계좌번호</param>
        ///<param name="arg2">비밀번호 : 사용안함(공백)</param>
        ///<param name="arg3">종목코드 : 전문 조회할 종목코드</param>
        ///<param name="arg4">주문가격 : </param>
        public void GetOPW20013(string callbackID, string arg1, string arg2, string arg3, string arg4)
        {
            nowCallbackID = callbackID;
            SetInputValue("계좌번호", arg1);
            SetInputValue("비밀번호", arg2);
            SetInputValue("종목코드", arg3);
            SetInputValue("주문가격", arg4);
            CommRqData("계좌미결제청산가능수량조회요청", "OPW20013", 0, GetScrNum());
        }

        ///<summary> 코드명:OPW20014 기능명:선옵실시간증거금산출요청</summary>
        ///<param name="arg1">계좌번호 : 전문 조회할 보유계좌번호</param>
        ///<param name="arg2">비밀번호 : 사용안함(공백)</param>
        ///<param name="arg3">KOSPI200지수 : 소수점제거한값(123.45 -> 12345)</param>
        ///<param name="arg4">비밀번호입력매체구분 : 00</param>
        public void GetOPW20014(string callbackID, string arg1, string arg2, string arg3, string arg4)
        {
            nowCallbackID = callbackID;
            SetInputValue("계좌번호", arg1);
            SetInputValue("비밀번호", arg2);
            SetInputValue("KOSPI200지수", arg3);
            SetInputValue("비밀번호입력매체구분", arg4);
            CommRqData("선옵실시간증거금산출요청", "OPW20014", 0, GetScrNum());
        }

        ///<summary> 코드명:OPW20015 기능명:옵션매도주문증거금현황요청</summary>
        ///<param name="arg1">월물구분 : 결제월(YYYYMM 연도4자리, 월 2자리)</param>
        ///<param name="arg2">클래스구분 : 01:KOSPI200, 11:삼성전자, 13:POSCO, 15:한국전력, 16:현대차, 19:기아차, 24:LG전자, 40:하나금융지주, 45:LGD, 46:KB금융, 50:SK하이닉스, 75:미국달러</param>
        public void GetOPW20015(string callbackID, string arg1, string arg2)
        {
            nowCallbackID = callbackID;
            SetInputValue("월물구분", arg1);
            SetInputValue("클래스구분", arg2);
            CommRqData("옵션매도주문증거금현황요청", "OPW20015", 0, GetScrNum());
        }

        ///<summary> 코드명:OPW20016 기능명:신용융자 가능종목요청</summary>
        ///<param name="arg1">신용종목등급구분 : A:A군, B:B군, C:C군</param>
        ///<param name="arg2">시장거래구분 : %:전체, 1:코스피, 0:코스닥</param>
        ///<param name="arg3">종목번호 : 조회시작하는 종목코드, 공백가능</param>
        public void GetOPW20016(string callbackID, string arg1, string arg2, string arg3)
        {
            nowCallbackID = callbackID;
            SetInputValue("신용종목등급구분", arg1);
            SetInputValue("시장거래구분", arg2);
            SetInputValue("종목번호", arg3);
            CommRqData("신용융자 가능종목요청", "OPW20016", 0, GetScrNum());
        }

        ///<summary> 코드명:OPW20017 기능명:신용융자 가능문의</summary>
        ///<param name="arg1">종목번호 : 조회하려는 종목코드</param>
        public void GetOPW20017(string callbackID, string arg1)
        {
            nowCallbackID = callbackID;
            SetInputValue("종목번호", arg1);
            CommRqData("신용융자 가능문의", "OPW20017", 0, GetScrNum());
        }

        #endregion

        #region API Functions

        ///<summary> 코드명:LGI10001 기능명:로그인시도</summary>
        public string GetLGI10001()
        {
            AxKHOpenAPI.CommConnect();
            IsReady = true;
            return "true";
        }

        ///<summary> 코드명:LGI10002 기능명:로그인상태표시</summary>
        public string GetLGI10002()
        {
            return AxKHOpenAPI.GetConnectState() + "";
        }

        ///<summary> 코드명:LGI10003 기능명:로그인정보표시</summary>
        ///<param name="arg1">태그 : ACCOUNT_CNT(보유계좌수), ACCLIST(연결된 보유계좌 목록), USER_ID(사용자 아이디), USER_NAME(사용자 이름), KEY_BSECGB(키보드보안해지여부 1:정상 2:해지), FIREW_SECGB(방화벽 설정여부 0:미설정 1:설정 2:해지), GetServerGubun(접속서버 구분 1: 모의투자 나머지:실서버)</param>
        public string GetLGI10003(string arg1)
        {
            return AxKHOpenAPI.GetLoginInfo(arg1) + "";
        }

        ///<summary> 코드명:REQ10001 기능명:실시간데이터해지</summary>
        ///<param name="arg1">코드 : API코드</param>
        public void GetREQ10001(string arg1)
        {
            AxKHOpenAPI.DisconnectRealData(arg1);
        }

        ///<summary> 코드명:ORD10001 기능명:거래주문</summary>
        ///<param name="arg1">계좌번호 : 계좌번호10자리</param>
        ///<param name="arg2">주문유형 : 1:신규매수, 2:신규매도 3:매수취소, 4:매도취소, 5:매수정정, 6:매도정정</param>
        ///<param name="arg3">종목코드 : 종목코드</param>
        ///<param name="arg4">주문수량 : 주문수량</param>
        ///<param name="arg5">주문가격 : 주문가격</param>
        ///<param name="arg6">호가구분 : 00:지정가,03:시장가,05:조건부지정가,06:최유리지정가,07:최우선지정가,10:지정가IOC,13:시장가IOC,16:최유리IOC,20:지정가FOK,23:시장가FOK,26:최유리FOK,61:장전시간외종가,62:시간외단일가매매,81:장후시간외종가</param>
        ///<param name="arg7">원주문번호 : 신규주문에는 공백, 정정(취소)주문할 원주문번호를 입력합니다.</param>
        public string GetORD10001(string callbackID , string arg1, string arg2, string arg3, string arg4, string arg5, string arg6, string arg7)
        {
            nowCallbackID = callbackID;
            string screenCode = "0101";
            //4550089011|1|048550|1|1730|03|
            logger.Debug("call:"+callbackID);
            logger.Debug("arg1:"+arg1);
            logger.Debug("arg2:"+arg2);
            logger.Debug("arg3:"+arg3);
            logger.Debug("arg4:"+arg4);
            logger.Debug("arg5:"+arg5);
            logger.Debug("arg6:"+arg6);
            logger.Debug("arg7:"+arg7);
            return AxKHOpenAPI.SendOrder("주식주문", screenCode, arg1, Int32.Parse(arg2), arg3, Int32.Parse(arg4), Int32.Parse(arg5), arg6, arg7) + "";
        }

        ///<summary> 코드명:ORD10002 기능명:코스피200선물옵션,주식선물전용</summary>
        ///<param name="arg1">사용자구분 : 주식주문</param>
        ///<param name="arg2">화면번호 : 9998</param>
        ///<param name="arg3">계좌번호 : 계좌번호10자리</param>
        ///<param name="arg4">종목코드 : 종목코드</param>
        ///<param name="arg5">주문종류 : 1:신규매매,2:정정:,3:취소</param>
        ///<param name="arg6">매매구분 : 1:매도,2:매수</param>
        ///<param name="arg7">거래구분 : 1:지정가,2:조건부지정가,3:시장가,4:최유리지정가,5:지정가IOC,6:지정가FOK,7:시장가IOC,8:시장가FOK,9:최유리지정가IOC,A:최유리지정가FOK</param>
        ///<param name="arg8">주문수량 : 주문수량</param>
        ///<param name="arg9">주문가격 : 주문가격</param>
        ///<param name="arg10">원주문번호 : 원주문번호</param>
        public string GetORD10002(string arg1, string arg2, string arg3, string arg4, string arg5, string arg6, string arg7, string arg8, string arg9, string arg10)
        {
            string screenCode = "9998";
            return AxKHOpenAPI.SendOrderFO(arg1, screenCode, arg3, arg4, Int32.Parse(arg5), arg6, arg7, Int32.Parse(arg8), arg9, arg10) + "";
        }

        ///<summary> 코드명:ORD10003 기능명:거래신용주문</summary>
        ///<param name="arg1">사용자구분 : 주식주문</param>
        ///<param name="arg2">화면번호 : 9997</param>
        ///<param name="arg3">계좌번호 : 계좌번호10자리</param>
        ///<param name="arg4">주문유형 : 1:신규매수, 2:신규매도 3:매수취소, 4:매도취소, 5:매수정정, 6:매도정정</param>
        ///<param name="arg5">종목코드 : 종목코드</param>
        ///<param name="arg6">주문수량 : 주문수량</param>
        ///<param name="arg7">주문가격 : 주문가격</param>
        ///<param name="arg8">호가구분 : 00:지정가,03:시장가,05:조건부지정가,06:최유리지정가,07:최우선지정가,10:지정가IOC,13:시장가IOC,16:최유리IOC,20:지정가FOK,23:시장가FOK,26:최유리FOK,61:장전시간외종가,62:시간외단일가매매,81:장후시간외종가</param>
        ///<param name="arg9">신용거래구분 : 03:신용매수 - 자기융자,33:신용매도 - 자기융자,99:신용매도 자기융자 합</param>
        ///<param name="arg10">대출일 : 대출일(YYYYMMDD) 신용매도 - 자기융자 일때는 종목별 대출일을 입력하고 신용매도 - 융자합이면 "99991231"을 입력합니다.</param>
        ///<param name="arg11">원주문번호 : 원주문번호</param>
        public string GetORD10003(string arg1, string arg2, string arg3, string arg4, string arg5, string arg6, string arg7, string arg8, string arg9, string arg10, string arg11)
        {
            string screenCode = "9997";
            return AxKHOpenAPI.SendOrderCredit(arg1, screenCode, arg3, Int32.Parse(arg4), arg5, Int32.Parse(arg6), Int32.Parse(arg7), arg8, arg9, arg10, arg11) + "";
        }

        ///<summary> 코드명:CDT10001 기능명:조건검색목록조회</summary>
        public string GetCDT10001()
        {
            return AxKHOpenAPI.GetConditionLoad() + "";
        }

        ///<summary> 코드명:CDT10002 기능명:조건검색요청</summary>
        ///<param name="arg1">화면번호 : 9996</param>
        ///<param name="arg2">조건식이름 : 조건식 목록이 "0^조건식1;3^조건식1;8^조건식3;23^조건식5"일때 조건식3을 검색</param>
        ///<param name="arg3">조건식인덱스 : 조건식 인덱스는 조건식 이름과 함께 전달한 조건명 인덱스를 그대로 사용해야 합니다.</param>
        ///<param name="arg4">조회구분 : 0:조건검색,1:실시간 조건검색</param>
        public string GetCDT10002(string arg1, string arg2, string arg3, string arg4)
        {
            string screenCode = "9996";
            return AxKHOpenAPI.SendCondition(screenCode, arg2, Int32.Parse(arg3), Int32.Parse(arg4)) + "";
        }

        ///<summary> 코드명:CDT10003 기능명:조건검색 중지</summary>
        ///<param name="arg1">화면번호 : 9995</param>
        ///<param name="arg2">조건식이름 : 조건식 목록이 "0^조건식1;3^조건식1;8^조건식3;23^조건식5"일때 조건식3을 검색</param>
        ///<param name="arg3">조건식인덱스 : 조건식 인덱스는 조건식 이름과 함께 전달한 조건명 인덱스를 그대로 사용해야 합니다.</param>
        public void GetCDT10003(string arg1, string arg2, string arg3)
        {
            string screenCode = "9995";
            AxKHOpenAPI.SendConditionStop(screenCode, arg2, Int32.Parse(arg3));
        }

        ///<summary> 코드명:CDT10004 기능명:실시간시세등록</summary>
        ///<param name="arg1">화면번호 : 0150</param>
        ///<param name="arg2">종목코드 : 종목코드리스트</param>
        ///<param name="arg3">실시간FID : 실시간FID리스트</param>
        ///<param name="arg4">실시간등록타입 : 0:등록된 종목른 실시간해지.등록한 종목만 실시간 시세 등록 ,1:먼저 등록한 종목들과 함께 실시간 시세 등록</param>
        public string GetCDT10004(string arg1, string arg2, string arg3, string arg4)
        {
            string screenCode = "0150";
            return AxKHOpenAPI.SetRealReg(screenCode, arg2, arg3, arg4) + "";
        }

        ///<summary> 코드명:CDT10005 기능명:실시간시세해지</summary>
        ///<param name="arg1">화면번호 : 0150 또는 ALL</param>
        ///<param name="arg2">종목코드 : 종목코드 또는 ALL</param>
        public void GetCDT10005(string arg1, string arg2)
        {
            AxKHOpenAPI.SetRealRemove(arg1, arg2);
        }

        ///<summary> 코드명:CST10001 기능명:종목코드조회</summary>
        ///<param name="arg1">시장구분값 : 0:장내,10:코스닥,3:ELW,8:ETF,50:KONEX,4:뮤추얼펀드,5:신주인수권,6:리츠,9:하이얼펀드,30:K-OTC 만일 시장구분값이 NULL이면 전체 시장코드를 전달합니다.</param>
        public string GetCST10001(string arg1)
        {
            return AxKHOpenAPI.GetCodeListByMarket(arg1) + "";
        }

        ///<summary> 코드명:CST10002 기능명:종목코드의종목명</summary>
        ///<param name="arg1">종목코드 : 종목코드</param>
        public string GetCST10002(string arg1)
        {
            return AxKHOpenAPI.GetMasterCodeName(arg1) + "";
        }

        ///<summary> 코드명:CST10003 기능명:종목상장주식수조회</summary>
        ///<param name="arg1">종목코드 : 종목코드</param>
        public string GetCST10003(string arg1)
        {
            return AxKHOpenAPI.GetMasterListedStockCnt(arg1) + "";
        }

        ///<summary> 코드명:CST10004 기능명:종목감리구분조회</summary>
        ///<param name="arg1">종목코드 : 종목코드</param>
        public string GetCST10004(string arg1)
        {
            return AxKHOpenAPI.GetMasterConstruction(arg1) + "";
        }

        ///<summary> 코드명:CST10005 기능명:종목상장일조회</summary>
        ///<param name="arg1">종목코드 : 종목코드</param>
        public string GetCST10005(string arg1)
        {
            return AxKHOpenAPI.GetMasterListedStockDate(arg1) + "";
        }

        ///<summary> 코드명:CST10006 기능명:증거금비율,거래정지,관리종목,감리종목,투자융의종목,담보대출,액면분할,신용가능조회</summary>
        ///<param name="arg1">종목코드 : 종목코드</param>
        public string GetCST10006(string arg1)
        {
            return AxKHOpenAPI.GetMasterStockState(arg1) + "";
        }

        ///<summary> 코드명:CST10007 기능명:회원사정보조회</summary>
        public string GetCST10007()
        {
            return AxKHOpenAPI.GetBranchCodeName() + "";
        }

        ///<summary> 코드명:CST10008 기능명:지수선물종목코드리스트조회</summary>
        public string GetCST10008()
        {
            return AxKHOpenAPI.GetFutureList() + "";
        }

        ///<summary> 코드명:CST10009 기능명:지수옵션행사가조회</summary>
        public string GetCST10009()
        {
            return AxKHOpenAPI.GetActPriceList() + "";
        }

        ///<summary> 코드명:CST10010 기능명:지수옵션월물정보조회</summary>
        public string GetCST10010()
        {
            return AxKHOpenAPI.GetMonthList() + "";
        }

        ///<summary> 코드명:CST10011 기능명:지수옵션코드조회</summary>
        ///<param name="arg1">행사가 : 소수점을 포함한 행사가</param>
        ///<param name="arg2">콜풋구분값 : 2:콜, 3:풋</param>
        ///<param name="arg3">월물 : YYYYMM 6자리 월물</param>
        public string GetCST10011(string arg1, string arg2, string arg3)
        {
            return AxKHOpenAPI.GetOptionCode(arg1, Int32.Parse(arg2), arg3) + "";
        }

        ///<summary> 코드명:CST10012 기능명:지수옵션소수점제거한ATM값조회</summary>
        public string GetCST10012()
        {
            return AxKHOpenAPI.GetOptionATM() + "";
        }

        ///<summary> 코드명:CST10013 기능명:주식선물종목조회</summary>
        ///<param name="arg1">기초자산구분 : 기초자산구분</param>
        public string GetCST10013(string arg1)
        {
            return AxKHOpenAPI.GetSFutureList(arg1) + "";
        }



        #endregion
    }
}
