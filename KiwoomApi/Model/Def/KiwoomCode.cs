using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiwoomCode
{
    public enum Log
    {
        조회,     // 조회창 출력
        에러,     // 에러창 출력
        일반,     // 일반창 출력
        실시간    // 실시간창 출력
    }

    class KOAErrorCode
    {
        public const int OP_ERR_NONE = 0;    // 정상처리                                                                    
        public const int OP_ERR_FAIL = -10;    // 실패
        public const int OP_ERR_COND_NUM = -11;    // 조건번호 없슴                                                                
        public const int OP_ERR_COND_NUM_FOMUL = -12;    // 조건번호와 조건식 불일치                                                     
        public const int OP_ERR_COND_REQ_OVER = -13;    // 조건검색 조회요청 초과                                                       
        public const int OP_ERR_USER_INFO_FAIL = -100;     // 사용자정보교환 실패                                                           
        public const int OP_ERR_SERVER_CONNECT_FAIL = -101;     // 서버 접속 실패                                                                
        public const int OP_ERR_VER_FAIL = -102;     // 버전처리 실패                                                                 
        public const int OP_ERR_FIREWALL_FAIL = -103;     // 개인방화벽 실패                                                               
        public const int OP_ERR_MEM_FAIL = -104;     // 메모리 보호실패                                                               
        public const int OP_ERR_FUNC_PARAM_FAIL = -105;     // 함수입력값 오류                                                               
        public const int OP_ERR_CONNECT_FAIL = -106;     // 통신연결 종료                                                                 
        public const int OP_ERR_SEQURITY_FAIL = -107;     // 보안모듈 오류                                                                 
        public const int OP_ERR_CERT_LOGIN = -108;     // 공인인증 로그인 필요                                                                                    
        public const int OP_ERR_PRICE_REQ_OVER = -200;     // 시세조회 과부하                                                               
        public const int OP_ERR_TR_RESET_FAIL = -201;     // 전문작성 초기화 실패.                                                         
        public const int OP_ERR_TR_PARAM_FAIL = -202;     // 전문작성 입력값 오류.                                                         
        public const int OP_ERR_NONE_DATA = -203;     // 데이터 없음.                                                                  
        public const int OP_ERR_REQ_ITEM_OVER = -204;     // 조회가능한 종목수 초과. 한번에 조회 가능한 종목개수는 최대 100종목.           
        public const int OP_ERR_DATA_RECIEVE_FAIL = -205;     // 데이터 수신 실패                                                              
        public const int OP_ERR_FID_MAX_FAIL = -206;     // 조회가능한 FID수 초과. 한번에 조회 가능한 FID개수는 최대 100개.               
        public const int OP_ERR_REAL_DISCONNECT_FAIL = -207;     // 실시간 해제오류                                                               
        public const int OP_ERR_PRICE_OVER = -209;     // 시세조회제한                                                                    
        public const int OP_ERR_PARAM_FAIL = -300;     // 입력값 오류                                                                   
        public const int OP_ERR_PASSWORD_FAIL = -301;     // 계좌비밀번호 없음.                                                            
        public const int OP_ERR_OTHER_ACC_USE = -302;     // 타인계좌 사용오류.                                                            
        public const int OP_ERR_MIS_2BILL_EXC = -303;     // 주문가격이 주문착오 금액기준 초과.                                                     
        public const int OP_ERR_MIS_5BILL_EXC = -304;     // 주문가격이 주문착오 금액기준 초과.                                                     
        public const int OP_ERR_MIS_1PER_EXC = -305;     // 주문수량이 총발행주수의 1% 초과오류.                                          
        public const int OP_ERR_MID_3PER_EXC = -306;     // 주문수량은 총발행주수의 3% 초과오류.                                          
        public const int OP_ERR_ORDER_TRANS_FAIL = -307;     // 주문전송 실패                                                                 
        public const int OP_ERR_ORDER_TRANS_OVER = -308;     // 주문전송 과부하                                                               
        public const int OP_ERR_ORDER_VALUE_300_FAIL = -309;     // 주문수량 300계약 초과.                                                        
        public const int OP_ERR_ORDER_VALUE_500_FAIL = -310;     // 주문수량 500계약 초과.                                                        
        public const int OP_ERR_ORDER_LIMIT_MAX = -311;     // 주문전송제한 과부하
        public const int OP_ERR_NONE_CHE = -340;     // 계좌정보 없음.                                                                
        public const int OP_ERR_NONE_CODE = -500;     // 종목코드 없음.              
    }


    public class KOACode
    {

        /// <summary>
        /// 주문코드 클래스
        /// </summary>
        public struct OrderType
        {
            private string Name;
            private int Code;

            public OrderType(int nCode, string strName)
            {
                this.Name = strName;
                this.Code = nCode;
            }

            public string name
            {
                get
                {
                    return this.Name;
                }
            }

            public int code
            {
                get
                {
                    return this.Code;
                }
            }
        }

        public readonly static OrderType[] orderType = new OrderType[6];


        /// <summary>
        /// 호가구분 클래스
        /// </summary>
        public struct HogaGb
        {
            private string Name;
            private string Code;

            public HogaGb(string strCode, string strName)
            {
                this.Code = strCode;
                this.Name = strName;
            }

            public string name
            {
                get
                {
                    return this.Name;
                }
            }

            public string code
            {
                get
                {
                    return this.Code;
                }
            }
        }

        public readonly static HogaGb[] hogaGb = new HogaGb[13];

        public struct MarketCode
        {
            private string Name;
            private string Code;

            public MarketCode(string strCode, string strName)
            {
                this.Code = strCode;
                this.Name = strName;
            }

            public string name
            {
                get
                {
                    return this.Name;
                }
            }

            public string code
            {
                get
                {
                    return this.Code;
                }
            }
        }

        public readonly static MarketCode[] marketCode = new MarketCode[9];

        static KOACode()
        {
            // 주문타입 설정
            orderType[0] = new OrderType(1, "신규매수");
            orderType[1] = new OrderType(2, "신규매도");
            orderType[2] = new OrderType(3, "매수취소");
            orderType[3] = new OrderType(4, "매도취소");
            orderType[4] = new OrderType(5, "매수정정");
            orderType[5] = new OrderType(6, "매도정정");

            // 호가타입 설정
            hogaGb[0] = new HogaGb("00", "지정가");
            hogaGb[1] = new HogaGb("03", "시장가");
            hogaGb[2] = new HogaGb("05", "조건부지정가");
            hogaGb[3] = new HogaGb("06", "최유리지정가");
            hogaGb[4] = new HogaGb("07", "최우선지정가");
            hogaGb[5] = new HogaGb("10", "지정가IOC");
            hogaGb[6] = new HogaGb("13", "시장가IOC");
            hogaGb[7] = new HogaGb("16", "최유리IOC");
            hogaGb[8] = new HogaGb("20", "지정가FOK");
            hogaGb[9] = new HogaGb("23", "시장가FOK");
            hogaGb[10] = new HogaGb("26", "최유리FOK");
            hogaGb[11] = new HogaGb("61", "시간외단일가매매");
            hogaGb[12] = new HogaGb("81", "시간외종가");

            // 마켓코드 설정
            marketCode[0] = new MarketCode("0", "장내");
            marketCode[1] = new MarketCode("3", "ELW");
            marketCode[2] = new MarketCode("4", "뮤추얼펀드");
            marketCode[3] = new MarketCode("5", "신주인수권");
            marketCode[4] = new MarketCode("6", "리츠");
            marketCode[5] = new MarketCode("8", "ETF");
            marketCode[6] = new MarketCode("9", "하이일드펀드");
            marketCode[7] = new MarketCode("10", "코스닥");
            marketCode[8] = new MarketCode("30", "제3시장");
        }
    }


    class Error
    {
        private static string errorMessage;

        Error()
        {
            errorMessage = "";
        }

        ~Error()
        {
            errorMessage = "";
        }

        public static string GetErrorMessage()
        {
            return errorMessage;
        }

        public static bool IsError(int nErrorCode)
        {
            bool bRet = false;

            switch (nErrorCode)
            {
                case KOAErrorCode.OP_ERR_NONE:
                    errorMessage = "[" + nErrorCode.ToString() + "] :" + "정상처리";
                    bRet = true;
                    break;
                case KOAErrorCode.OP_ERR_FAIL: errorMessage = "[" + nErrorCode.ToString() + "] :" + "실패"; break;
                case KOAErrorCode.OP_ERR_COND_NUM: errorMessage = "[" + nErrorCode.ToString() + "] :" + "조건번호 없슴"; break;
                case KOAErrorCode.OP_ERR_COND_NUM_FOMUL: errorMessage = "[" + nErrorCode.ToString() + "] :" + "조건번호와 조건식 불일치"; break;
                case KOAErrorCode.OP_ERR_COND_REQ_OVER: errorMessage = "[" + nErrorCode.ToString() + "] :" + "조건검색 조회요청 초과"; break;
                case KOAErrorCode.OP_ERR_USER_INFO_FAIL: errorMessage = "[" + nErrorCode.ToString() + "] :" + "사용자정보교환 실패"; break;
                case KOAErrorCode.OP_ERR_SERVER_CONNECT_FAIL: errorMessage = "[" + nErrorCode.ToString() + "] :" + "서버 접속 실패"; break;
                case KOAErrorCode.OP_ERR_VER_FAIL: errorMessage = "[" + nErrorCode.ToString() + "] :" + "버전처리 실패"; break;
                case KOAErrorCode.OP_ERR_FIREWALL_FAIL: errorMessage = "[" + nErrorCode.ToString() + "] :" + "개인방화벽 실패"; break;
                case KOAErrorCode.OP_ERR_MEM_FAIL: errorMessage = "[" + nErrorCode.ToString() + "] :" + "메모리 보호실패"; break;
                case KOAErrorCode.OP_ERR_FUNC_PARAM_FAIL: errorMessage = "[" + nErrorCode.ToString() + "] :" + "함수입력값 오류"; break;
                case KOAErrorCode.OP_ERR_CONNECT_FAIL: errorMessage = "[" + nErrorCode.ToString() + "] :" + "통신연결 종료"; break;
                case KOAErrorCode.OP_ERR_SEQURITY_FAIL: errorMessage = "[" + nErrorCode.ToString() + "] :" + "보안모듈 오류"; break;
                case KOAErrorCode.OP_ERR_CERT_LOGIN: errorMessage = "[" + nErrorCode.ToString() + "] :" + "공인인증 로그인 필요"; break;
                case KOAErrorCode.OP_ERR_PRICE_REQ_OVER: errorMessage = "[" + nErrorCode.ToString() + "] :" + "시세조회 과부하"; break;
                case KOAErrorCode.OP_ERR_TR_RESET_FAIL: errorMessage = "[" + nErrorCode.ToString() + "] :" + "전문작성 초기화 실패."; break;
                case KOAErrorCode.OP_ERR_TR_PARAM_FAIL: errorMessage = "[" + nErrorCode.ToString() + "] :" + "전문작성 입력값 오류."; break;
                case KOAErrorCode.OP_ERR_NONE_DATA: errorMessage = "[" + nErrorCode.ToString() + "] :" + "데이터 없음."; break;
                case KOAErrorCode.OP_ERR_REQ_ITEM_OVER: errorMessage = "[" + nErrorCode.ToString() + "] :" + "조회가능한 종목수 초과. 한번에 조회 가능한 종목개수는 최대 100종목."; break;
                case KOAErrorCode.OP_ERR_DATA_RECIEVE_FAIL: errorMessage = "[" + nErrorCode.ToString() + "] :" + "데이터 수신 실패"; break;
                case KOAErrorCode.OP_ERR_FID_MAX_FAIL: errorMessage = "[" + nErrorCode.ToString() + "] :" + "조회가능한 FID수 초과. 한번에 조회 가능한 FID개수는 최대 100개."; break;
                case KOAErrorCode.OP_ERR_REAL_DISCONNECT_FAIL: errorMessage = "[" + nErrorCode.ToString() + "] :" + "실시간 해제오류"; break;
                case KOAErrorCode.OP_ERR_PRICE_OVER: errorMessage = "[" + nErrorCode.ToString() + "] :" + "시세조회제한"; break;
                case KOAErrorCode.OP_ERR_PARAM_FAIL: errorMessage = "[" + nErrorCode.ToString() + "] :" + "입력값 오류"; break;
                case KOAErrorCode.OP_ERR_PASSWORD_FAIL: errorMessage = "[" + nErrorCode.ToString() + "] :" + "계좌비밀번호 없음."; break;
                case KOAErrorCode.OP_ERR_OTHER_ACC_USE: errorMessage = "[" + nErrorCode.ToString() + "] :" + "타인계좌 사용오류."; break;
                case KOAErrorCode.OP_ERR_MIS_2BILL_EXC: errorMessage = "[" + nErrorCode.ToString() + "] :" + "주문가격이 주문착오 금액기준 초과."; break;
                case KOAErrorCode.OP_ERR_MIS_5BILL_EXC: errorMessage = "[" + nErrorCode.ToString() + "] :" + "주문가격이 주문착오 금액기준 초과."; break;
                case KOAErrorCode.OP_ERR_MIS_1PER_EXC: errorMessage = "[" + nErrorCode.ToString() + "] :" + "주문수량이 총발행주수의 1% 초과오류."; break;
                case KOAErrorCode.OP_ERR_MID_3PER_EXC: errorMessage = "[" + nErrorCode.ToString() + "] :" + "주문수량은 총발행주수의 3% 초과오류."; break;
                case KOAErrorCode.OP_ERR_ORDER_TRANS_FAIL: errorMessage = "[" + nErrorCode.ToString() + "] :" + "주문전송 실패"; break;
                case KOAErrorCode.OP_ERR_ORDER_TRANS_OVER: errorMessage = "[" + nErrorCode.ToString() + "] :" + "주문전송 과부하"; break;
                case KOAErrorCode.OP_ERR_ORDER_VALUE_300_FAIL: errorMessage = "[" + nErrorCode.ToString() + "] :" + "주문수량 300계약 초과."; break;
                case KOAErrorCode.OP_ERR_ORDER_VALUE_500_FAIL: errorMessage = "[" + nErrorCode.ToString() + "] :" + "주문수량 500계약 초과."; break;
                case KOAErrorCode.OP_ERR_ORDER_LIMIT_MAX: errorMessage = "[" + nErrorCode.ToString() + "] :" + "주문전송제한 과부하"; break;
                case KOAErrorCode.OP_ERR_NONE_CHE: errorMessage = "[" + nErrorCode.ToString() + "] :" + "계좌정보 없음."; break;
                case KOAErrorCode.OP_ERR_NONE_CODE: errorMessage = "[" + nErrorCode.ToString() + "] :" + "종목코드 없음."; break; ;
                default:
                    errorMessage = "[" + nErrorCode.ToString() + "] :" + "알려지지 않은 오류입니다.";
                    break;
            }

            return bRet;
        }
    }
}
