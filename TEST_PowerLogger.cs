static void TEST_PowerLogger(Args _args)
{
    PowerLogger::instance().LogOnDB().write("Sample error text.", funcName(), 0, "My sample title");
}