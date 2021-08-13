class LogOnAOSFile implements IWritable
{
}

private void new()
{
}

public void write(Notes text, str context="", boolean status=0, Notes title= funcName())
{
    Session             s = new Session();
    Filename            fileName, hostFolderName;
    TextIo              textIo;
    str                 userName;
    Set                 permissionSet;
    #File
    ;
    // Set the file path on server
    fileName = @"C:\\DAXDev_Log.txt";
    userName=curUserId();

    // Set the permission contract for writing to file
    permissionSet =  new Set(Types::Class);
    permissionSet.add(new FileIOPermission(fileName, #IO_APPEND));

    //Assert the permissions
    CodeAccessPermission::assertMultiple(permissionSet);

    // Write the content
    textIo = new TextIo(fileName, #IO_APPEND);
    textIo.write(strFmt("%1, %2, %3, %4 | %5", title, text, context, status, userName));

    // Take back the permissions
    CodeAccessPermission::revertAssert();
 }
 
 public static LogOnAOSFile instance()
{
    LogOnAOSFile   singleton;
    SysGlobalCache  globalCache = infolog.objectOnServer() ? appl.globalCache() : infolog.globalCache();
    ;

    if (globalCache.isSet(classStr(LogOnAOSFile), 0))

        singleton = globalCache.get(classStr(LogOnAOSFile), 0);

    else
    {
        singleton = new LogOnAOSFile();
        infoLog.globalCache().set(classStr(LogOnAOSFile), 0, singleton);
        appl.globalCache().set(classStr(LogOnAOSFile), 0, singleton);

    }
    return singleton;
}