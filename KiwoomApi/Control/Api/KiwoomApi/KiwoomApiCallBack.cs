using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiwoomApi.Control.Api
{
    class OpenApiCallBack
    {

        public void AxKHOpenAPI_OnReceiveChejanData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveChejanDataEvent e)
        {

        }

        public void AxKHOpenAPI_OnReceiveConditionVer(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveConditionVerEvent e)
        {

        }

        public void AxKHOpenAPI_OnReceiveInvestRealData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveInvestRealDataEvent e)
        {

        }

        public void AxKHOpenAPI_OnReceiveMsg(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveMsgEvent e)
        {

        }

        public void AxKHOpenAPI_OnReceiveRealCondition(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealConditionEvent e)
        {

        }

        public void AxKHOpenAPI_OnReceiveRealData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealDataEvent e)
        {

        }

        public void AxKHOpenAPI_OnReceiveTrCondition(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrConditionEvent e)
        {

        }

        public void AxKHOpenAPI_OnReceiveTrData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e)
        {

        }

        public int OnCommConnect() { return 0; }
        public string OnCommGetData(string sJongmokCode, string sRealType, string sFieldName, int nIndex, string sInnerFieldName) { return null; }
        public int OnCommInvestRqData(string sMarketGb, string sRQName, string sScreenNo) { return 0; }
        public int OnCommKwRqData(string sArrCode, int bNext, int nCodeCount, int nTypeFlag, string sRQName, string sScreenNo) { return 0; }
        public int OnCommRqData(string sRQName, string sTrCode, int nPrevNext, string sScreenNo) { return 0; }
        public void OnCommTerminate() { return; }
        public void OnDisconnectRealData(string sScnNo) { return; }
        public string OnGetActPriceList() { return null; }
        public string OnGetAPIModulePath() { return null; }
        public string OnGetBranchCodeName() { return null; }
        public string OnGetChejanData(int nFid) { return null; }
        public string OnGetCodeListByMarket(string sMarket) { return null; }
        public string OnGetCommData(string strTrCode, string strRecordName, int nIndex, string strItemName) { return null; }
        public object OnGetCommDataEx(string strTrCode, string strRecordName) { return null; }
        public string OnGetCommRealData(string sTrCode, int nFid) { return null; }
        public int OnGetConditionLoad() { return 0; }
        public string OnGetConditionNameList() { return null; }
        public int OnGetConnectState() { return 0; }
        public int OnGetDataCount(string strRecordName) { return 0; }
        public string OnGetFutureCodeByIndex(int nIndex) { return null; }
        public string OnGetFutureList() { return null; }
        public string OnGetLoginInfo(string sTag) { return null; }
        public int OnGetMarketType(string sTrCode) { return 0; }
        public string OnGetMasterCodeName(string sTrCode) { return null; }
        public string OnGetMasterConstruction(string sTrCode) { return null; }
        public string OnGetMasterLastPrice(string sTrCode) { return null; }
        public int OnGetMasterListedStockCnt(string sTrCode) { return 0; }
        public string OnGetMasterListedStockDate(string sTrCode) { return null; }
        public string OnGetMasterStockState(string sTrCode) { return null; }
        public string OnGetMonthList() { return null; }
        public string OnGetOptionATM() { return null; }
        public string OnGetOptionCode(string strActPrice, int nCp, string strMonth) { return null; }
        public string OnGetOptionCodeByActPrice(string sTrCode, int nCp, int nTick) { return null; }
        public string OnGetOptionCodeByMonth(string sTrCode, int nCp, string strMonth) { return null; }
        public string OnGetOutputValue(string strRecordName, int nRepeatIdx, int nItemIdx) { return null; }
        public int OnGetRepeatCnt(string sTrCode, string sRecordName) { return 0; }
        public string OnGetSActPriceList(string strBaseAssetGb) { return null; }
        public string OnGetSFOBasisAssetList() { return null; }
        public string OnGetSFutureCodeByIndex(string strBaseAssetCode, int nIndex) { return null; }
        public string OnGetSFutureList(string strBaseAssetCode) { return null; }
        public string OnGetSMonthList(string strBaseAssetGb) { return null; }
        public string OnGetSOptionATM(string strBaseAssetGb) { return null; }
        public string OnGetSOptionCode(string strBaseAssetGb, string strActPrice, int nCp, string strMonth) { return null; }
        public string OnGetSOptionCodeByActPrice(string strBaseAssetGb, string sTrCode, int nCp, int nTick) { return null; }
        public string OnGetSOptionCodeByMonth(string strBaseAssetGb, string sTrCode, int nCp, string strMonth) { return null; }
        public string OnGetThemeGroupCode(string strThemeCode) { return null; }
        public string OnGetThemeGroupList(int nType) { return null; }
        public string OnKOA_Functions(string sFunctionName, string sParam) { return null; }
        public int OnSendCondition(string strScrNo, string strConditionName, int nIndex, int nSearch) { return 0; }
        public void OnSendConditionStop(string strScrNo, string strConditionName, int nIndex) { return; }
        public int OnSendOrder(string sRQName, string sScreenNo, string sAccNo, int nOrderType, string sCode, int nQty, int nPrice, string sHogaGb, string sOrgOrderNo) { return 0; }
        public int OnSendOrderCredit(string sRQName, string sScreenNo, string sAccNo, int nOrderType, string sCode, int nQty, int nPrice, string sHogaGb, string sCreditGb, string sLoanDate, string sOrgOrderNo) { return 0; }
        public int OnSendOrderFO(string sRQName, string sScreenNo, string sAccNo, string sCode, int lOrdKind, string sSlbyTp, string sOrdTp, int lQty, string sPrice, string sOrgOrdNo) { return 0; }
        public int OnSetInfoData(string sInfoData) { return 0; }
        public void OnSetInputValue(string sID, string sValue) { return; }
        public int OnSetOutputFID(string sID) { return 0; }
        public int OnSetRealReg(string strScreenNo, string strCodeList, string strFidList, string strOptType) { return 0; }
        public void OnSetRealRemove(string strScrNo, string strDelCode) { return; }
    }
}
