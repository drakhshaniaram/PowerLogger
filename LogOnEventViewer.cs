class LogOnEventViewer implements IWritable
{
}

private void new()
{
}

public void write(Notes text, str context="", boolean status=0, Notes title= funcName())
{
// ------------------------------------------------(c) 2020 Last
// System: Microsoft Dynamics AX For Developers
// Description: متدی جهت لاگ کردن خروجی های ثقیل الصید!
// Parameters:      txt, logType
// Return value:    None
// Exceptions:      None
// Pre-conditions:  None
// Post-conditions: None
// Creator: Jalal Derakhshani
// Creation date:   02.03.2020
// -----------------------------------------------------------------
// -----------------------------------------------------------------
    System.Diagnostics.EventLog eventlog;
    #Define.LogSource("Application")
    #Define.LogName("DAXDev")

    // check if the log already exists
    if(!System.Diagnostics.EventLog::SourceExists(#LogSource))
    {
        // create new log
        System.Diagnostics.EventLog::CreateEventSource(#LogSource, #LogName);
    }

    eventlog = new System.Diagnostics.EventLog();
    eventlog.set_Source(#LogSource);


    switch(status){
        case 1:
            // write info entry
            eventlog.WriteEntry(strFmt("%1\n %2\n %3\n %4", title, text, context, status));
            break;
        case 0:
            // write error entry
            eventlog.WriteEntry(strFmt("%1\n %2\n %3\n %4", title, text, context, status) +
                "\n\n------------------\n" + con2Str(xSession::xppCallStack()), System.Diagnostics.EventLogEntryType::Error);
            break;
    }


}

public static LogOnEventViewer instance()
{
    LogOnEventViewer   singleton;
    SysGlobalCache  globalCache = infolog.objectOnServer() ? appl.globalCache() : infolog.globalCache();
    ;

    if (globalCache.isSet(classStr(LogOnEventViewer), 0))

        singleton = globalCache.get(classStr(LogOnEventViewer), 0);

    else
    {
        singleton = new LogOnEventViewer();
        infoLog.globalCache().set(classStr(LogOnEventViewer), 0, singleton);
        appl.globalCache().set(classStr(LogOnEventViewer), 0, singleton);

    }
    return singleton;
}