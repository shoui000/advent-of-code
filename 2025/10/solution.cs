using System;
using System.Collections.Generic;
using System.Numerics;

namespace AdventOfCode_2025_day10;

class Program {

	static void Main(string[] args) {
		int option = 0;

		if (args.Length > 0){
			option = int.Parse(args[0]);
		}

		Solution s = new Solution(option);
		long fpSolution = s.FirstPuzzle();
		// int spSolution = s.SecondPuzzle();

		Console.WriteLine($"FirstPuzzle: The answer is {fpSolution}");
		// Console.WriteLine($"SecondPuzzle: The answer is {spSolution}");

	}

}

class Solution {
	private string[] input;

	public Solution(int op) {
		switch (op) {
			case 0: 
				input = File.ReadAllLines("./input_test.txt");
				break;
			case 1:
				input = File.ReadAllLines("./input.txt");
				break;
			default:
				input = [];
				break;
		}
	}

	public long FirstPuzzle() {
		long sum = 0;

		foreach (string line in input) {
			Machine m = new Machine(line);

			sum += BitOperations.PopCount(m.FindSolution());
		}

		return sum;
	}
}

class Machine {
	public UInt16 FinalState { get; }
	public List<Button> Buttons { get; }

	public Machine(string input) {
		this.FinalState = 0;
		this.Buttons = new List<Button>();
		string[] info = input.Split(" ");

		for (int i = info[0][1..^1].Length - 1; i >= 0; i--) {
			if (info[0][1..^1][i] == '#')
				this.FinalState = (UInt16)(this.FinalState | (1 << i));
		}

		for (int i = 1; i < info.Length-1; i++) {
			this.Buttons.Add(new Button(info[i]));
		}
	}

	public UInt16 FindSolution() {
		int solutionsQtt = (1 << this.Buttons.Count) - 1;
		List<UInt16> validSolutions = new List<ushort>();
		UInt16 state = 0, number = 0;
		int j = 0;

		for (UInt16 i = 1; i <= solutionsQtt; i++) {
			state = 0; number = i; j = 0;

			while (number > 0) {
				if ((number & 1) == 1)
					state ^= this.Buttons[j].Layout;

				j++;
				number >>= 1;
			}

			if (state == this.FinalState)
				validSolutions.Add(i);
		}

		validSolutions.Sort((n1, n2) => BitOperations.PopCount(n1).CompareTo(
					BitOperations.PopCount(n2)));

		return validSolutions[0];
	}
}

class Button {
	public UInt16 Layout { get; }

	public Button(string input) {
		string[] pos = input[1..^1].Split(",");

		this.Layout = 0;

		foreach (string s in pos) {
			UInt16 mask = UInt16.Parse(s);

			this.Layout = (UInt16)(this.Layout | (1 << mask));
		}
	}

	public override string ToString() {
		return Convert.ToString(this.Layout, 2);
	}
}
