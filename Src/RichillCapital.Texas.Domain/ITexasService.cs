﻿using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Texas.Domain;

public interface ITexasService
{
    Result<Session> NewSession();
    Result AddPlayer(string id, string name);
    Result BuyIn(int groups = 1);
    Result CashOut(int finalChipValue);
    Result CloseSession();
}