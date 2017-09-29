using System;
using Splat;

namespace Mobile.ViewModels
{
	public class Logger : ILogger
	{
		public LogLevel Level { get; set; } = LogLevel.Debug;

		public void Write([Localizable(false)] string message, LogLevel logLevel)
		{
			System.Diagnostics.Debug.WriteLine(message);
			this.WriteCallCount = this.WriteCallCount + 1;
		}

		public int WriteCallCount { get; private set; }
	}
}
