using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiwoomApi.Control.Api
{
    class OpenApiCallBack
    {

        public void axKHOpenAPI_OnReceiveChejanData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveChejanDataEvent e)
        {

        }

        public void axKHOpenAPI_OnReceiveConditionVer(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveConditionVerEvent e)
        {

        }

        public void axKHOpenAPI_OnReceiveInvestRealData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveInvestRealDataEvent e)
        {

        }

        public void axKHOpenAPI_OnReceiveMsg(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveMsgEvent e)
        {

        }

        public void axKHOpenAPI_OnReceiveRealCondition(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealConditionEvent e)
        {

        }

        public void axKHOpenAPI_OnReceiveRealData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealDataEvent e)
        {

        }

        public void axKHOpenAPI_OnReceiveTrCondition(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrConditionEvent e)
        {

        }

        public void axKHOpenAPI_OnReceiveTrData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e)
        {

        }

        public int onCommConnect() { return 0; }
        public string onCommGetData(string sJongmokCode, string sRealType, string sFieldName, int nIndex, string sInnerFieldName) { return null; }
        public int onCommInvestRqData(string sMarketGb, string sRQName, string sScreenNo) { return 0; }
        public int onCommKwRqData(string sArrCode, int bNext, int nCodeCount, int nTypeFlag, string sRQName, string sScreenNo) { return 0; }
        public int onCommRqData(string sRQName, string sTrCode, int nPrevNext, string sScreenNo) { return 0; }
        public void onCommTerminate() { return; }
        public void onDisconnectRealData(string sScnNo) { return; }
        public string onGetActPriceList() { return null; }
        public string onGetAPIModulePath() { return null; }
        public string onGetBranchCodeName() { return null; }
        public string onGetChejanData(int nFid) { return null; }
        public string onGetCodeListByMarket(string sMarket) { return null; }
        public string onGetCommData(string strTrCode, string strRecordName, int nIndex, string strItemName) { return null; }
        public object onGetCommDataEx(string strTrCode, string strRecordName) { return null; }
        public string onGetCommRealData(string sTrCode, int nFid) { return null; }
        public int onGetConditionLoad() { return 0; }
        public string onGetConditionNameList() { return null; }
        public int onGetConnectState() { return 0; }
        public int onGetDataCount(string strRecordName) { return 0; }
        public string onGetFutureCodeByIndex(int nIndex) { return null; }
        public string onGetFutureList() { return null; }
        public string onGetLoginInfo(string sTag) { return null; }
        public int onGetMarketType(string sTrCode) { return 0; }
        public string onGetMasterCodeName(string sTrCode) { return null; }
        public string onGetMasterConstruction(string sTrCode) { return null; }
        public string onGetMasterLastPrice(string sTrCode) { return null; }
        public int onGetMasterListedStockCnt(string sTrCode) { return 0; }
        public string onGetMasterListedStockDate(string sTrCode) { return null; }
        public string onGetMasterStockState(string sTrCode) { return null; }
        public string onGetMonthList() { return null; }
        public string onGetOptionATM() { return null; }
        public string onGetOptionCode(string strActPrice, int nCp, string strMonth) { return null; }
        public string onGetOptionCodeByActPrice(string sTrCode, int nCp, int nTick) { return null; }
        public string onGetOptionCodeByMonth(string sTrCode, int nCp, string strMonth) { return null; }
        public string onGetOutputValue(string strRecordName, int nRepeatIdx, int nItemIdx) { return null; }
        public int onGetRepeatCnt(string sTrCode, string sRecordName) { return 0; }
        public string onGetSActPriceList(string strBaseAssetGb) { return null; }
        public string onGetSFOBasisAssetList() { return null; }
        public string onGetSFutureCodeByIndex(string strBaseAssetCode, int nIndex) { return null; }
        public string onGetSFutureList(string strBaseAssetCode) { return null; }
        public string onGetSMonthList(string strBaseAssetGb) { return null; }
        public string onGetSOptionATM(string strBaseAssetGb) { return null; }
        public string onGetSOptionCode(string strBaseAssetGb, string strActPrice, int nCp, string strMonth) { return null; }
        public string onGetSOptionCodeByActPrice(string strBaseAssetGb, string sTrCode, int nCp, int nTick) { return null; }
        public string onGetSOptionCodeByMonth(string strBaseAssetGb, string sTrCode, int nCp, string strMonth) { return null; }
        public string onGetThemeGroupCode(string strThemeCode) { return null; }
        public string onGetThemeGroupList(int nType) { return null; }
        public string onKOA_Functions(string sFunctionName, string sParam) { return null; }
        public int onSendCondition(string strScrNo, string strConditionName, int nIndex, int nSearch) { return 0; }
        public void onSendConditionStop(string strScrNo, string strConditionName, int nIndex) { return; }
        public int onSendOrder(string sRQName, string sScreenNo, string sAccNo, int nOrderType, string sCode, int nQty, int nPrice, string sHogaGb, string sOrgOrderNo) { return 0; }
        public int onSendOrderCredit(string sRQName, string sScreenNo, string sAccNo, int nOrderType, string sCode, int nQty, int nPrice, string sHogaGb, string sCreditGb, string sLoanDate, string sOrgOrderNo) { return 0; }
        public int onSendOrderFO(string sRQName, string sScreenNo, string sAccNo, string sCode, int lOrdKind, string sSlbyTp, string sOrdTp, int lQty, string sPrice, string sOrgOrdNo) { return 0; }
        public int onSetInfoData(string sInfoData) { return 0; }
        public void onSetInputValue(string sID, string sValue) { return; }
        public int onSetOutputFID(string sID) { return 0; }
        public int onSetRealReg(string strScreenNo, string strCodeList, string strFidList, string strOptType) { return 0; }
        public void onSetRealRemove(string strScrNo, string strDelCode) { return; }
    }
}
