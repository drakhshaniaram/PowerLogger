/// <summary>
/// A highly-reusable class for logging in database, both in single mode and and bulk mode.
/// It takes advantage of singleton design pattern for preventing waste of memory
/// It also has the ability to log the PowerDetector::instance().* classes
///
/// Created and maintained By Jalal Derakhshani
/// drakhshani.aram@hotmail.com
/// 2020
/// </summary>
///
class LogOnDB implements IWritable
{
}

private void new()
{
}

/// <summary>
/// This method has the ability to log the entries through a unique connection instance to the AX transactional database in an intact way,
/// so the wrapper transaction getting failed cannot intercept this entries from getting logged.
/// This ensures the user that logging will take place.
/// </summary>
///
public void write(Notes text, str context="", boolean status=0, Notes title= funcName())
{
    LogOnDBTable logOnDBTable;
    utcdatetime                 dateTime;

    // Error handling
    str                         finalMsg = "";
    SysInfologEnumerator        infoLogEnum;
    SysInfologMessageStruct     infoMessageStruct;
    UserConnection              connection = new UserConnection();

    dateTime = DateTimeUtil::utcNow();

    appl.dbSynchronize(tableName2id("LogOnDBTable"), true); // Ensures that the imported table has been synchronized and ready to use.

    try{
        ttsBegin;

        logOnDBTable.setConnection(connection);
        logOnDBTable.ttsbegin();
        logOnDBTable.clear();
        logOnDBTable.Title = title;
        logOnDBTable.StatusOfOp = status;
        logOnDBTable.ErrorMessageText = text;
        logOnDBTable.ErrorType = context;
        logOnDBTable.doInsert();

        logOnDBTable.ttscommit();

        ttsCommit;
    }catch(Exception::Error){
                infoLogEnum = SysInfologEnumerator::newData(infolog.infologData());
                while(infoLogEnum.moveNext())
                {
                    infoMessageStruct = SysInfologMessageStruct::construct(infoLogEnum.currentMessage());
                    finalMsg+=infoMessageStruct.message();
                }
                logOnDBTable.ttsabort();
                ttsabort;

        }
}

public static LogOnDB instance()
{
    LogOnDB   singleton;
    SysGlobalCache  globalCache = infolog.objectOnServer() ? appl.globalCache() : infolog.globalCache();
    ;

    if (globalCache.isSet(classStr(LogOnDB), 0))

        singleton = globalCache.get(classStr(LogOnDB), 0);

    else
    {
        singleton = new LogOnDB();
        infoLog.globalCache().set(classStr(LogOnDB), 0, singleton);
        appl.globalCache().set(classStr(LogOnDB), 0, singleton);

    }
    return singleton;
}