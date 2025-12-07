using System;
using System.Text;
using System.Collections.Generic;

namespace AdventOfCode_2025_day06;

class Program {
	static void Main(string[] args) {
		int option = 0;

		if (args.Length > 0){
			option = int.Parse(args[0]);
		}

		Solution s = new Solution(option);
		long fpSolution = s.FirstPuzzle();
		long spSolution = s.SecondPuzzle();

		Console.WriteLine($"FirstPuzzle: The answer is {fpSolution}");
		Console.WriteLine($"SecondPuzzle: The answer is {spSolution}");
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

		List<int[]> columns = new List<int[]>();
		int columnsCount = 0, number = 0;
		long localsum = 0;

		for (int i = 0; i < input.Length; i++) {
			string[] c = input[i].Split(" ");

			columnsCount = 0;
			for (int j = 0; j < c.Length; j++) {
				if (string.IsNullOrWhiteSpace(c[j])) continue;

				if (i == input.Length-1)
					number = c[j][0];
				else 
					number = int.Parse(c[j]);

				if (columnsCount >= columns.Count) {
					columns.Add(new int[input.Length]);
				}

				columns[columnsCount][i] = number;
				columnsCount++;
			}
		}

		foreach (int[] c in columns) {
			if (c[^1] == '*') 
				localsum = 1;
			else if (c[^1] == '+')
				localsum = 0;

			for (int i = 0; i < c.Length-1; i++) {
				if (c[^1] == '*')
					localsum *= c[i];
				else if (c[^1] == '+')
					localsum += c[i];
			}

			sum += localsum;
		}
		
		return sum;
	}

	public long SecondPuzzle() {
		long sum = 0;

		int rows = input.Length, cols = input[0].Length;

		char[,] fMatrix = new char[rows, cols];

		for (int i = 0; i < rows; i++) {
			for (int j = 0; j < input[i].Length; j++) {
				fMatrix[i, j] = input[i][j];
			}
		}

		List<string> colsBuffer = new List<string>();

		StringBuilder buffer = new StringBuilder();
		char option = '0';
		long localsum = 0;

		for (int j = 0; j < cols; j++) {
			for (int i = 0; i < rows; i++) {

				if (fMatrix[i, j] == '+') {
					option = '+';
					continue;
				} else if (fMatrix[i, j] == '*') {
					option = '*';
					continue;
				}

				if (buffer != null)
					buffer.Append(fMatrix[i, j]);
			}

			if (buffer == null || string.IsNullOrWhiteSpace(buffer.ToString())) {
				if (option == '+')
					localsum = 0;
				else if (option == '*')
					localsum = 1;

				foreach (string n in colsBuffer) {
					if (option == '+')
						localsum += long.Parse(n.Trim());
					else if (option == '*')
						localsum *= long.Parse(n.Trim());
				}

				sum += localsum;

				if (buffer != null)
					buffer.Clear();
				colsBuffer.Clear();
				option = '0';

				continue;
			}

			colsBuffer.Add(buffer.ToString());
			if (buffer != null)
				buffer.Clear();
		}

		if (option == '+')
			localsum = 0;
		else if (option == '*')
			localsum = 1;

		foreach (string n in colsBuffer) {
			if (option == '+')
				localsum += long.Parse(n.Trim());
			else if (option == '*')
				localsum *= long.Parse(n.Trim());
		}

		sum += localsum;

		return sum;
	}
}
