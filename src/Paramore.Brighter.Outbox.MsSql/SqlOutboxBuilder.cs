#region Licence
/* The MIT License (MIT)
Copyright © 2014 Francesco Pighi <francesco.pighi@gmail.com>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the “Software”), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE. */

#endregion

namespace Paramore.Brighter.Outbox.MsSql
{
    /// <summary>
    /// Provide SQL statement helpers for creation of an Outbox
    /// </summary>
    public class SqlOutboxBuilder
    {
        const string OutboxDdl = @"
        CREATE TABLE {0}
            (
              [Id] [BIGINT] NOT NULL IDENTITY ,
              [MessageId] UNIQUEIDENTIFIER NOT NULL ,
              [Topic] NVARCHAR(255) NULL ,
              [MessageType] NVARCHAR(32) NULL ,
              [Timestamp] DATETIME NULL ,
              [CorrelationId] UNIQUEIDENTIFIER NULL,
              [ReplyTo] NVARCHAR(255) NULL,
              [ContentType] NVARCHAR(128) NULL,  
              [Dispatched] DATETIME NULL,
              [HeaderBag] NTEXT NULL ,
              [Body] NTEXT NULL ,
              PRIMARY KEY ( [Id] )
            );
        ";
        
        private const string OutboxExistsSQL = @"SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'{0}'";

        /// <summary>
        /// Gets the DDL statements required to create an Outbox in MSSQL
        /// </summary>
        /// <param name="outboxTableName">The name of the Outbox table</param>
        /// <returns>The required DDL</returns>
        public static string GetDDL(string outboxTableName)
        {
            return string.Format(OutboxDdl, outboxTableName);
        }
        
        /// <summary>
        /// Get the SQL statements required to test for the existence of an Inbox in MSSQL
        /// </summary>
        /// <param name="inboxTableName">The name that was used for the Inbox table</param>
        /// <returns>The required SQL</returns>
        public static string GetExistsQuery(string inboxTableName)
        {
            return string.Format(OutboxExistsSQL, inboxTableName);
        }
    }
}
