//----------------------------------------------------------------------------------------
//	Copyright © 2003 - 2021 Tangible Software Solutions, Inc.
//	This class can be used by anyone provided that the copyright notice remains intact.
//
//	This class allows the following VB My.Computer properties to be called from C#:
//	My.Computer.Audio
//	My.Computer.Clock
//	My.Computer.Info
//	My.Computer.Keyboard
//	My.Computer.Mouse
//	My.Computer.Network
//	My.Computer.Ports
//
//	Notes:
//	1. A reference to the Microsoft.VisualBasic assembly is required.
//	2. Calls to My.Computer.Clipboard, My.Computer.FileSystem, My.Computer.Name,
//	My.Computer.Registry, and My.Computer.Screen are converted inline.
//----------------------------------------------------------------------------------------

using Microsoft.VisualBasic.Devices;

namespace PES_CPK_Solution.My
{
	public static class Computer
	{
		public static readonly Audio Audio = new Audio();
		public static readonly Clock Clock = new Clock();
		public static readonly ComputerInfo Info = new ComputerInfo();
		public static readonly Keyboard Keyboard = new Keyboard();
		public static readonly Mouse Mouse = new Mouse();
		public static readonly Network Network = new Network();
		public static readonly Ports Ports = new Ports();
	}
}