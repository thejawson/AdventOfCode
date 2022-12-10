﻿namespace AdventOfCode;
class Day3 : IDay
{
    public string Puzzle1() => Input.Split("\r\n").Select(m => Priority(m)).Sum().ToString();

    public string Puzzle2()
    {
        var elfGroup = Input.Split("\r\n");
        int priority = 0;
        for (int i = 0; i < elfGroup.Length / 3; i++)
            priority += Priority2(elfGroup.Skip(i * 3).Take(3).ToArray());

        return priority.ToString();
    }

    private static int Priority(string bag)
    {
        var compartment1 = bag.Take(bag.Length / 2);
        var compartment2 = bag.TakeLast(bag.Length / 2);
        foreach (var item in compartment1)
            if (compartment2.Contains(item))
                return PriorityScore(item);

        return 0;
    }

    private static int Priority2(string[] bags)
    {
        foreach (var item in bags[0])
            if (bags[1].Contains(item) && bags[2].Contains(item))
                return PriorityScore(item);

        return 0;
    }

    private static int PriorityScore(char item)
    {
        var ascii = (int)item;

        return ascii < 97 ? ascii - 38 : ascii - 96;
    }

    const string Input = @"qFdBBvtHHfvRlfvsqldvqjPpQmnQmjnjjjTRTLGRNG
ZCWhhCsJCzSJzSbzgsmPTGNNPPNGjgLTLjgn
WJZsbJMwJcszJcScwhVltFwBFBlqddvFdHDfqq
crtTsGTtqFThGQGCrsjTwdNJwpRdnJJwffRClpSf
PWVBPVHLvHHVgvZWBzmPpnfRSJJRQnSRflRPSNSl
gmzBzDgzmZQWLDLLgVmDrqGhsscrqDMGhcqtqcFr
HsbcdVrsbVbcLfPqqQsqqtPj
mMBFzZRnmFMRBDnDFBGZDGdDqLjtdQtPtgfPfttgtqgq
BZvZZdJMBFdJhSvhbhchcHll
GNRSqRfcNTpfGCcqjfzBpDQPWBzgDpQsPWzW
rrSdnVHlbMdLdBDzgtBtBmQt
rbFwwnLFLFwlMLrFwFhMVLrGNSTfZTRhfTqjGJRRZTCNcf
QWTnQCnWNNWmTnSPQwmqDbcscbpcjPjVPbrjpq
vJhzZNlNNgdzgzJdlGzHHcHDpjsHqrvbVrbvrD
RzRdRlhLgtCwCWSLnN
SFTJFTTwTVVSJBnSTdvNNfWbZCZWNZCNNhBv
srLrcHDcsjtLcLLcrLctjlcvbDNhmWCvNhZWGZZhNvhZmb
rclgtMPrrSgVTgJCng
DbrhDzcDffbzNbZvZWSSqSTNSVWv
gCPltPmCPglFnPFwtGPhGPwTCTdZZWZVRvWqdRqVVdTdvR
hLBhlmlstcffBzrpfj
wFLLmhMfwZLDwmMNRhZwRLDvJgldbJHPdQvcQHHJQPgH
bjrVrTSSJdQHcVll
CGCSsCCBpspBrqbSttpbqWmWZRmfFRZhZMNNLFqFLm
zWGjjBHGjzzTWMjhtDDWtPPlJZPJpvqQrmZTqQQpmr
RFbVLcBVLRcRVcCsCCqvpCZqmplqQJmPrlvQ
FLNRRSSRgScSVLLLNdFdwjHjnftBtGMgMjzHgzjWjj
znVSqnqbqzSbzTHqDDZmlcFcnhDMnDmn
LtjsvdvLJdjfFwRRCCMlChwCpMcclCcZ
LgvjjfjFQVgNTgWq
SJRJRFFCMSsGRMMwtZJRCVTgqgTVgTBCVpjTjmmWlB
ccvnnpnDVqTcBVTV
vPHprdHdpnzHSMsSrMRZJGws
GddGrcGNHnGvnCHddvCSWqTSWsTwTWShbHlhhb
gDPzLRVZgQfpRRFQDDVFDfzhSzsTBqqqnqbhnWTSSlST
QVFfFgRQQgLtgffZRfpFPfntjrcrjCmtCdMMmjMdJJJNtm
jjmNcpGCNmDqqsBfnZnGGGRLsZ
lrmlVWlQQtWllgtbQVrWBnZsJgsRLfZLhZBBBffL
rWMVQtrFlbFlSSMHVSdHHNHdcdDcddzppzzm
bTpjpjcVTLmphbLppJwqzqwJLqqzzzgRLJ
sdHNbrvNHrqPvZZZPRww
bNQCrCNtNsSlhffhVhpVWFCW
lpNnpMMZZDbNbnBjcrbjvScFmbGj
wqhdqVqdscrjdLsv
HQftVqWCfhwqtCCjWwfqzzVPZRJQgMlggZMMMZTNMNTnNRTN
fvvGbFtVmtTwgtMT
WcCcClzPCCcczJJScPWWZzBDmwbhBBHSghgDDMTHMDBD
nWPljWzZWnbcbRsNFjFFdFdVjFsj
NQrcLNmQGRfGLHHLZgbbnpjZJJJndbgnlv
DWtThDWtzzhltWTwjbdpvjbgqjgg
VtSPFWtBPBFSFBWCStshWBmlRfHfMRcfQLQLLlmCrCcN
pbmwqJnqSJVwwDPCjZZzrZfD
QtssBTvNdNvNtQvQGpGhdjPjDjczZDfjhgPPDcgjgr
GltptQpMGNNpRWlWFVFHJFHLWH
ZLLsDGGVhZcQQLhrLshrVFwHnWqJnWMnJJJnqfWfGn
jMlPdTlPlgCgFpngFWFnJfpw
TlTNbdSSTSTmTjPMTCdBPjBMrLDczsZcNrDhRNDRQRLLRVVz
HDLpBqDVVTvwGDDNRT
PlVWjfhsPMMmWtlFNTrhrrvCCCTNNbvw
lsglfgVJmsfMjJfSqSzdZnLgqcnLnp
pfCDJWBpfDffpJLgQJzzVzNrgNgNgNhNzmVr
ZnnGZbGTPZnsnRFdTlbrwdrNzrrmmWwmwVwttH
GbPGRvTnZljWnpqSMMCjqJQSCf
ZgnFgwggznFrfrwfHhNMMr
pctLCLRhPHBLMLWfBL
JJcdJcQCCJmQJppmlgndnFslsVnsvghZ
WpMgTppWGSWWJmJDpJcJJhqm
zZzjZNHvNjPvNsbZLbRLzsPcqhVJSVttdwhwmdRhtdJRVd
sLbvvCZCPSSSbbPfNlQQTQGBllCTnMnWQn
fwbwswddwSbBfDBggMBPDPhHcPWDmhHhmWnWPC
FQFlzLCzQTlrTTzvltFqFrmhPHjnhhnnchcJWcRRmRRq
lpLzlFZzCltrTNlTztQLZfSMGBNdSBVwbBNVSMSbVs
FMmgbTFdgLSgFQdjrRPrQBPDdj
ZqqWRvsfGrrPvvPC
wZzwnqccRwRNNpRSMztSMMFbgzLTFS
qTwBPfTfqQDMDrssHdvtRHccHMjR
gWSZGWzGFhnFFgnhNsRHtRdsVjZcRjHs
jgplhpJJFgnDrrwfqprwDP
CWhMSRfWhVVnRSZnVVdsLQqQMzGqLBvGMQqczv
PHbpNwrjJplttvcclLlQzzDszc
NrJbJrFNNJNPrmwrtbjtNmCfSWfWhSZZfSWCsfShfFVR
VLhRPLGLRPRSStRRLwfGqfmDwbmqbqqDlD
rBSFvppnzTbwDwlDcFWm
MJrnJTMvMsrTsPtshRNPZdSLhL
BZBrRCrnCQBBnZfGqhGGMMRcthMhMG
TLjsCdDCPTvNssjdsPsDgsgqGcPHczchtHczWzPWzzlWhG
gsTpsdNbvNNjNSpsNDTsmCnSVQFmSFwZnQBnmnQQ
llbsNsWrmbrGbWCNtBjcCFBzQFZBCFjF
LdSpwgdqSgzwJdRdLMRHLjQQjHjFHctjHBDTZj
gSppgpSJMhpzwrhblfbhhlWlnW
DwhTvvsJZWsBnDzPpBLbFp
GHtNGRGNdzbMBBtmBt
NljlCSVSHdjGSQRGlCQSCswqfWzhZfTcfzcJvshJ
lmsGNFsDGqCbFQBbffjjwpzptw
hRQdvdrvrvSngWnvnHrTMfzfzRtftzwVTwwpzB
HnSSWrvLJvWJGFDsmFLPDFcQ
bwwpGphpLghpTvpWphvJlFLJqqltjSjVlSStSR
cmszZdDdBZzcNcDCDcNsmNMcqVjMJStFRJltVPVrlVPjVJll
HcdmcCzzzQcHNcsCdpnGnhwGgnRggHvbbR
CfMBbwBGbMbDCFrDvhFFDT
mjzRjjRdSmjPnzFZgnnrTT
cmSsVcHjLHTwMfLBpBpBwM
whqqfZzgHvhSzzVNVDbpDbmbVbNpJD
GcQFntGTCCcCTMCTGBlJsJsDDWpRbWBsJpNS
FnPcrGFFdddMnCnTqgqgqPHfLjLqgSzz
zMSzzjssFdGnszRtNftqqwFHbbZw
RRPLVrgrwHqBqgwt
rPWmLCTCQlCQQmmrWLrQShJshhzdhhJjcSjlzRds
lvgvCDfPqLHppqpCCDJncbntttbBtBBVHjwtrB
TdddszSQsWcngjzVbcVZ
hRWsTRTGQhNRGhRTFSWmlpgfqlvLmplPqvvGgv
LbWFLQdWWPwWSjSHPHRfppHHDRpggR
zmqqNNzlzmnzzNCmVCmtBzpfGsfpBgDgspprcfcfsrRB
qNNVNJtNmmmNzznVJzvCTDZWhvZZjZFbWQQhFhbZSw
DjdHqJVVhHVZjhDHPWtDtZLFBRBFmSRTFSbwmRRTffTTJf
NNznnGlgMQsnQzNclzpfSRSMRmfPMmFRwBwB
vzrcGcNcPPvHvHPt
wLCcmZwWTNtZNdMSMGSCnJGGMB
RFbHsPhVvFPRjlshhrnQnGjQGSdSqJfqnQBM
HhzVlFHhPwzScmSTgL
TNlBhDNvNBFpJgpPPpDQ
jjfCdCZZqsCZsbdqPgFGGMRzSFMqQMRS
jnWPtWssCtWcmZbbtstvnrrvhVBhTNNhBHlBlL
DZwNWPDzPVWbJngrQjrNnrQcMg
GRRfttLBhhvTvmLmFcFcgFFSnjWrnsrG
TLthBWtTRLHqhlLLfmhBqVPDJVdPwzJCPPZHwdDdVd
GGVhrVSMQwQqfVssVvnWFgvgWn
jtlcRBBtQRmpWsjzFCvzWnvF
QPcRbpppDmNDtPPblZMfhZdDwdMrqSSGrq
ZRrdtBdQvQsWnnfWFZsF
bJLcMzNDLbMgwfnGMWFv
lpvhmzNDmDmlNbzbmrVVPrHRCPHQBVCP
rZllQrsRWrlQswccMVbGbVbTdcQQ
NtJCntLSHCjznfLTcGGGqWMdWM
jCtzzSFthhSSSjPJrFDlvWrlDZRpwpRZ
mQmbLjbrLQjLmTtwwWBTTvWjtt
BHSqdHclHHNFlppNqWPwfwDvTfDPPtCw
ddSGMGHcdcMhMZnBbmbZmgGJJg
lvvBzvDnlzjfPnfjnQPlldRbVbRqbqqCgsqqVpQQgVqc
NNFtGNMtTNFmJNGNZtZMwVRTTcsCpVTbbgCbgRhscp
FGNGZMtNLWmmJWGFWJGLSNtPrPnBfDzzvjnDBzpnvDBLnv
fwvQRFQvQqwpwNJrwN
BstDnBjhjBhnshSptpJzWqNppbfr
CsDjCdZcBCDcjnfDHfhnfggZMGlgQVmgMTRmgVGMMl
MwlBVqVlsgnmzwJsvjhWZhGPvjvRRWzG
QNQpQpftHdHHCHGfSpCrQNdSrDRDhchhjvjcPrRRWrPvhZjv
LtLSCTSGfHGdGwswnqsggssTqV
qDDCHjzjznTvWshZQWfnZZ
PFFmmNMMtNMVFtczcFPJNrLhZwQZQsSvvSvWvGQQJQssss
tFzrrPPNNFlzVrpRTpblRDqjTpDC
DWDrrBdpmdpBrCgDthdtfcHsqJsCqscqwfsjzHcq
TNLNFNSTQNQTSnlMcczVJjVzsqLDDfJJ
TFPZQRvvlMSPPtRWDtmDRWrBGr
LWGVZdrvWdpLGWRsjPMsHmdHdHldlj
zJzznChzzzCSfTgMhCPDmlDCbmlsmjDDQj
nSTTJhJtnShNtzwhgNrGRRWZZRvMWMtVrqGp
PbPmtNmBbPlqBvqlDJBT
LpGVDzVpVZqqSTvq
pMnWGLRLRppnGpGndrGPtgDCjMPmbPgCQmPPNN
sqcZcbZZpcZspcCCRMmznWGWdLWhwDRGTTWggT
NjFSJgVHrvfVtrGzWdSznDwLSTLn
jFrBNVVjBFNvHrFHBlBFFpMslPgPcpMPmcQPPZCgpP
frddqsThtsTfTbPcvhsrbsRLpRBNRpmDpGmRGcRNLpGp
QWJHCJwWzlHZQZHQCJJRzRqnLDGRGpnGBRnNDN
CVwHCClJjQgWCZVZQgMwSdthjrqvrSPPhdbqtPhs
TvdphBBhhhCgdLNNJJJLWz
fVcsqRVrPcnJWgDnJN
JlqsRJtssZwqwVtPwltRPsHHbFTwTFbpjHhQjTQbvpTF
cQSnPDDQJGNzwnNpZb
RHDrssVRDHRgsRFHRlrVwzzpNGZlfZdppZdwGNZb
sHCHtDgtCjVVLFChqPMhBCMcSTqB
hdbQbqcCCQcqFbCbVdcWCQQlRMBtGlRHBtBMpHhpHThZMR
LLsSLLfgJPrgPnssnmlZtlZpHGHVGfZVtZpl
PvmvgmvvnzmrSsSLJDqDNzqFDQdDwzWWbV
HNNjnLbpLGHvWJDhdWWPpWDW
lVcSNgcSVclhRlPZPRCDCR
cqmSQrwwrrVSrtQFqVNmFwjQnvjHzBbLLGjfjzHTzvnH
QmvWVppPHQQvbbvmSHSpPzfzwnWMTZFFzwFMCzLnwT
jGBljlNNjgDtGDrNjjtjqqDRnMzRLnFzCFnMfRfMzCnttF
jqNrrGdJcdgLjqDqBrDQbbmhdQQmmpPbphmbVv
ZHQCggVHHRDWvbfjGptVtLvL
nnFwnwrDDMShnhFrFLLLpjvPlPGGtLGb
dcNSMhrrTDCBCsWgCTQW
HqDDLGtDdCnhfDnwnV
PmlJsJTPlbdBTzTnzhnnCCWWzV
lSPjMScggsScgjSMMbqHLFGrRLGHRZZtdrcG
ZVVtNNppdZSdLtCPqnHhqJJFtb
zgwwQBfwmGgSrDfgrrGBggzHCnbJbqbCJFnqhHBhnHHCqJ
rvrzfmlRrgDgmrzfggvwzvdjjcccLjMjVcVcsVLjVSZR
dppcLRHpphchhNhSddjzHzWQWQLtrMsrWQCWCsMZssCZ
JGfBfJJfBqvGVlVbDBwDBDBfZnrQsMQtssMttssDsQMWZncn
qPVwlgPBmjpPhcmS
zGPnzBgPzPnPlHZlDDHnZBNCvrtcjcjmMcFzNcNFmFdc
qQpfsLTTSspqTfJdmdCtMjdtjvJcmr
bfQqqSrswLLrfpLTqprfTnDVDVBBbgHPDHnhDPgDbV
JssTnsdFztZLdNJnNtTsLNZGqlbGFBqrGMHqHBcFBqMFMH
CCgSfgPSvSfhpShSRppCdfrlqGHGGcHmclmqbbqbqlPc
wvVSVjQSSQhRVvfQChvZZsdtJstjLNLZDJnLss
CmfNNNZNqDrnDjMhZM
gdczzGtdFcddtWQgGGMnVhnjJwnrJFDPTwMP
dlvcdzdHtzQSLRSfmhLSqv
ZpFFLcHFZZRRmJVZgD
PzhrtQntzcrjCRJtbtRgBsBRVR
zdzWfCzhQzlhWfWhlvpFNlpSqcMSHHMv
NrrMgMhNQhNjQrtqtPtwVtZpggPw
TfRLndnLFCRFTFbbRDHwpVqqBBwsHwZsfH
TJFRdLlRThrlcvZcvQ
scrwRVjbQvQBzsBC
gMfVqNnVmnCBQDTvdn
SMqhWqVlmWSmqMVRSJjjpcFrcLpJrR
HtSQHQntHsHMrtHnGfHQVVzLvSBSVvVVSFNJzzVN
cmPRmpqlpPmcgTlTpjJNjjVDvDRFNVVBFD
hlmCpmqmpgqpZTlcdQHFQfbHHZttwMQwtr
VpWgbgfwCjbftwVPPpGQFQhzTBQTBGPzqFTS
dbRbDcRrsnsRrLZmLRDZldDZqTNTGqqFFzGGhSTNTFTzNmNT
MlLdHlDDHrHclMMrCwgHCCwWwCbCCjjg
GGNLhfDMVcVrcGsT
jSJQFjHbwPFSvQSHwZFvHSHrqCCrrTsqBwNBrcBNsVTsqq
QjZSjZJZPvNRZJQPnSZbJZRWLfnmgDlmhdhWgWLdMdfmhM
CgGnzPNggCJtNTgTZTPZzZZvvcDcDDdqDFcJssJDHDqvHq
jhhrrLVlmLRRnRflfVbFHHHqdVsDqcvbHVDb
jWfWwrlmRRnQmPzZNGZPBNCQTB
NzDDhwNmhvtrGmNCvWRVbcRRVTcHHcVFTbwV
LgsPlLsQgQdJsLdldtpgFFTMbnFqTcMbHqFcPncq
dgsJsLLLggljrhtGNNtSjvGm
ptzSrZtzhsmmtPrhLFRFnjnnLMsnfLRL
HvwVDHwWWgGDGdHgqVDWDMnRnTjFNTNjfLJvRRRRRR
DwDgWgQbDDDHwBBBWdwQGVHhlhlZZSSmztfcppSBhzZcZp
CWmWRzlMJqWDWqCJbqDlCBBVLMQHVMGrfMVtQZrsLL
SnhPdFFPNZsBBdHtVQ
SSPcFFgnwnSpwvcSjwzCqRzTmJbpJCRBmbbD
wQbqGWWSqwrbGWWWGjbNMJPfgfnnDmPnPNLfjN
tJFztRZCvVRCztZFZRVgmMhmgNLfRfnmDPNPhm
BFCVZzpVFlHCdbQqcTcGlJbbSG
tttfLPZZQZTlZPHHPWgMVvBnjmvjnjgGBQ
FzcNDDDrNzprrrshprhFJtVGVnjtjGvnhvVnnjnjGM
RDqJNszDPfdqPtlT
QCJdMjCQbdBjSbTHDsbWDDwHTP
zlvlmqzqGfgdNzLldrHwwPGpWDrPGZWprr
gfVfRczVqcRzmdcSQMjQSQCSjQCQ
RhhCGhRBShjjRfpwppFTfFHZHZZD
qzdqzlnPPctPdmtPdTZbwQvvwqvHHvZpwZ
nVTVTcsWmWSRhhRVrGVB
GmshRMnzqRGsPNwMwcrrpcVV
CDCbFCLvCgfDSFLslgDpwpLtTwwcPtNNtTTprt
JvSFbSbbFllJlgDvlJbgdRhRdqzBGnzshZnRRRnHBZ
ShJhtcsvvvQbnnsccVTLVTppWqddpVnLWp
NdzPrPZgPMNNrmzpTzpTCjWfzCpzVL
dZgHmZRPPZRlZmrPDtDRccvbtQQbJbRS
wqjLjwhznhBLqLWGfvSlvcmlrJsqrtJTJJ
PwbpFPQDRCDrDJTrTmvs
gbZVFbZHgwHbCdpCRMnffNLhWnnzMdzLLW
RVVGSNTTRlNqHblBNB
JfwJMvLLZwLsMJwWMJfwLHBqzFlvzpBQcqzblFbBqblq
wMCZJsgJCCCnsMHrgLLjSPSVTgShtRjTPhRRmt
lmQSSWdMHHLWgWqD
ZZtVGGGJrJvGVCwfgHNLccmNFFcqtc
vrCGPhvrTPdRBnsRTmmp
dDMDDjzCQjwCCcDgjSLLLsLNlmpplN
FqrHFTFRLCLVFBmS
JhHJhHRThrfPZPvhnTZZbWdwdwDDWtDzJDtbMtCW
ghwDzJRDwHmPthncSPncLLsPcvnv
MWCrNTCHrMVjQQMQcSdnpTLnFFdTcnTc
qbWMfWfNrWVQWfbjVBbqMfwDtqzmhmRRzGhtHhHhRwZh
fmSmnjTjrlzGlTzJdH
BrhRRQMrgQvgFFhQQbwpFvGGdZqZJqpJqHVpJGJLHdLJ
ggbDwQMsvsMQrFMFcWSPSCPmSsPfnnmP
cmNVbMrrrjcHDRcvfW
wQGdFfSThFsLhhHWvDCWDCJRCCjd
LtpStGhqrrpnfnpp
bvcccTqbgvpGndJtgdsgNd
wDQwQhtQhQRmSmjsJndJdBBJBJnlLS
hwhmRrzFVjtwzDmrVFrvPCcCMVPPvfqpCCTVVb
jRrRNPNRWjPRWPRQNjQjThTCzBBzDCFBGzgDFGGQ
dnppLwmwCnvtlqltvtnTGBThGhdFZhgzDzGccD
MvnqpLlMqCqCHMjWPPHMSHSs
NNpNNvpvBdtTrMFFMhSSwzjzchzwhzwL
VVndHqflQZZZgHSLLhjzRSmZRhcR
glGgnqbQlngnWCGJpJprtrtFrdPPGs
WqwRjzGtRzZZRRGjWBJzjwmfMTHGGssTTDsrLmmmQLMD
SNdvSdFlSNNhSPFFcPFclbQQslHmfHTDsTQMLgDTmHQQ
CNcCFvpdnWpjWwJf
PVPnVHcnRncGZqbVzHVPnnLbSMjwrzWMjSwDtWwWtwWhwDWz
pTfsQCshCllpglWWSjBMSQSrMrjM
hvpvppggCpJTvTmshgfsmZRRHqbcLPHZmPLRnmPZ
LQbhVZZmZhZjBdbGmgHqnHTmvqgnnWHr
SzCfDFFNRfsSFFMFfvprvpWzqzgqTwHTvp
CDNDFJgMDSQhjVdPJLQG
plpdLdpjjrrHJJjLrrHLFdbzzCcvzgFgcwggzPMFvvcMhM
GRtSBQNsQlMPRzRlzw
ZSTtsmBlmjLLpnpH
hglGNVSdNSghzSgCBhDFLBMBtFMMFtHtbtLL
frQZccRcqGFmFHrJ
nvfGZwvTwGTfQwvfTwfgnCSlpdnzgzslppCsCV
snTSPbQnTTnQgbmsTJsLfZwjffhpLnGRjpGfjL
dcNWcNHHlNtWHHlCtltWNFNMLZwjpGfpmrZfrFprrRGpwZfp
HmdNWCmDMVvQPDgqJs
GGFtSngQLfnSnQffgPnRgFRGRwmRJvwbBbJDwjvTbjrwhJvJ
WHClslcNNWcqNWlCZdcHsVrThBwBjbhDTDBhrvDZJTwm
NWVqqcHHNpsNcNVdVlhCMlHQQMQQzLfzQPttFGPMLSLgtF";
}
