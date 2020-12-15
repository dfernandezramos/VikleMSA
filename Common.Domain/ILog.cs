// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
namespace Common.Domain
{
    /// <summary>
    /// This interface contains the definition of the logger service.
    /// </summary>
    public interface ILog
    {
        void Info(string message);
        void Info(object obj);
        void Info(string message, object obj);
        void Warning(string message);
        void Warning(object obj);
        void Warning(string message, object obj);
        void Error(string message);
        void Error(object obj);
        void Error(string message, object obj);
        void InitForDebug();
    }
}