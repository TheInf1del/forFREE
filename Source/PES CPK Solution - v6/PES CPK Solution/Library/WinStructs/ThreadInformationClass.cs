﻿namespace AntiDebugging.WinStructs
{
    public enum ThreadInformationClass
    {
        ThreadBasicInformation = 0,
        ThreadTimes = 1,
        ThreadPriority = 2,
        ThreadBasePriority = 3,
        ThreadAffinityMask = 4,
        ThreadImpersonationToken = 5,
        ThreadDescriptorTableEntry = 6,
        ThreadEnableAlignmentFaultFixup = 7,
        ThreadEventPairReusable = 8,
        ThreadQuerySetWin32StartAddress = 9,
        ThreadZeroTlsCell = 10,
        ThreadPerformanceCount = 11,
        ThreadAmILastThread = 12,
        ThreadIdealProcessor = 13,
        ThreadPriorityBoost = 14,
        ThreadSetTlsArrayAddress = 15,   // Obsolete
        ThreadIsIoPending = 16,
        ThreadHideFromDebugger = 17,
        ThreadBreakOnTermination = 18,
        ThreadSwitchLegacyState = 19,
        ThreadIsTerminated = 20,
        ThreadLastSystemCall = 21,
        ThreadIoPriority = 22,
        ThreadCycleTime = 23,
        ThreadPagePriority = 24,
        ThreadActualBasePriority = 25,
        ThreadTebInformation = 26,
        ThreadCSwitchMon = 27,   // Obsolete
        ThreadCSwitchPmu = 28,
        ThreadWow64Context = 29,
        ThreadGroupInformation = 30,
        ThreadUmsInformation = 31,   // UMS
        ThreadCounterProfiling = 32,
        ThreadIdealProcessorEx = 33,
        ThreadCpuAccountingInformation = 34,
        ThreadSuspendCount = 35,
        ThreadDescription = 38,
        ThreadActualGroupAffinity = 41,
        ThreadDynamicCodePolicy = 42,
    }
}