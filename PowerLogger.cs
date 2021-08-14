/// <summary>
/// A highly-reusable tool for logging in database/event viewer/filing system, all in single mode and and bulk mode.
/// It takes advantage of singleton design pattern for preventing waste of memory
/// It also has the ability to log the PowerDetector::instance().OveralErr() classes
///
/// Created and maintained By Jalal Derakhshani
/// drakhshani.aram@hotmail.com
/// 2020
/// </summary>

class PowerLogger
{
}

public LogOnAOSFile LogOnAOSFile()
{
    return LogOnAOSFile::instance();
}

public LogOnDB LogOnDB()
{
    return LogOnDB::instance();
}

public LogOnEventViewer LogOnEventViewer()
{
    return LogOnEventViewer::instance();
}

private void new()
{
}

public static PowerLogger instance()
{
    PowerLogger   singleton;
    SysGlobalCache  globalCache = infolog.objectOnServer() ? appl.globalCache() : infolog.globalCache();
    ;

    if (globalCache.isSet(classStr(PowerLogger), 0))

        singleton = globalCache.get(classStr(PowerLogger), 0);

    else
    {
        singleton = new PowerLogger();
        infoLog.globalCache().set(classStr(PowerLogger), 0, singleton);
        appl.globalCache().set(classStr(PowerLogger), 0, singleton);

    }
    return singleton;
}
