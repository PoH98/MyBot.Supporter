﻿namespace MyBot.Supporter.Main
{
    enum SocketConnectionResult
    {
        OK = 0,
        GeneralSocksServerFailure = 1,
        ConnectionNotAllowedByRuleset = 2,
        NetworkUnreachable = 3,
        HostUnreachable = 4,
        ConnectionRefused = 5,
        TTLExpired = 6,
        CommandNotSupported = 7,
        AddressTypeNotSupported = 8,
        UnknownError,
        AuthenticationError,
        ConnectionReset,
        ConnectionError,
        InvalidProxyResponse
    }
}