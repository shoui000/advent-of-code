using System;
using System.Numerics;
using System.Linq;

namespace AdventOfCode_2025_day01;

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
	private int pairs;

	public Solution(int op) {
		switch (op) {
			case 0: 
				input = File.ReadAllLines("./input_test.txt");
				pairs = 10;
				break;
			case 1:
				input = File.ReadAllLines("./input.txt");
				pairs = 1000;
				break;
			default:
				input = [];
				pairs = 0;
				break;
		}
	}

	public long FirstPuzzle() {
		long sum = 1;

		Vector3[] vetores = new Vector3[input.Length];
		List<Aresta> arestas = new List<Aresta>();
		List<Circuito> circuitos = new List<Circuito>();

		for (int i = 0; i < vetores.Length; i++) {
			string[] xyz = input[i].Split(",");
			vetores[i] = new Vector3(int.Parse(xyz[0]), int.Parse(xyz[1]), int.Parse(xyz[2]));
		}

		for (int i = 0; i < vetores.Length; i++) {
			for (int j = i+1; j < vetores.Length; j++) {
				arestas.Add(new Aresta(vetores[i], vetores[j]));
			}
		}

		arestas.Sort();

		bool contains = false;
		for (int i = 0; i < this.pairs; i++) {
			contains = false;

			for (int j = 0; j < circuitos.Count; j++) {
				if (circuitos[j].HasAresta(arestas[i])) {
					contains = true;
					break;
				}

				if (circuitos[j].HasVetors(arestas[i])) {

					for (int x = 0; x < circuitos.Count; x++) {
						if (x == j) continue;

						if (circuitos[x].HasVetors(arestas[i])) {
							circuitos[j].MergeCircuit(circuitos[x]);
							circuitos.Remove(circuitos[x]);
							break;
						}
					}

					circuitos[j].AddAresta(arestas[i]);
					contains = true;
					break;
				}
			}

			if (!contains) {
				circuitos.Add(new Circuito(arestas[i]));
			}
		}

		circuitos.Sort();

		for (int i = 0; i < 3; i++) {
			sum *= circuitos[i].Vetores.Count;
		}

		return sum;
	}
}

class Aresta : IComparable<Aresta> {
	public float Tamanho { get; }
	public Vector3 Vetor1 { get; }
	public Vector3 Vetor2 { get; }

	public Aresta(Vector3 v1, Vector3 v2) {
		Vetor1 = v1;
		Vetor2 = v2;
		Tamanho = Vector3.Distance(v1, v2);
	}

	public override string ToString() {
		return $"{this.Vetor1} --{this.Tamanho.ToString("F2")}-- {this.Vetor2}";
	}

	public int CompareTo(Aresta? other) {
		if (other is null) return 1;

		return this.Tamanho.CompareTo(other.Tamanho);
	}

	public override bool Equals(object? other) {
		if (other is Aresta a)
			return (this.Vetor1 == a.Vetor1 && this.Vetor2 == a.Vetor2) ||
				(this.Vetor1 == a.Vetor2 && this.Vetor2 == a.Vetor1);

		return false;
	}

    public override int GetHashCode() {
        return base.GetHashCode();
    }

}

class Circuito : IComparable<Circuito> {
	public List<Aresta> Arestas { get; }
	public List<Vector3> Vetores { get; }

	public Circuito(Aresta arestaInicial) {
		Vetores = new List<Vector3>();
		Arestas = new List<Aresta>();

		this.AddAresta(arestaInicial);
	}

	public Circuito(Vector3 vetorInicial) {
		Vetores = new List<Vector3>();
		Arestas = new List<Aresta>();

		Vetores.Add(vetorInicial);
	}

	public int CompareTo(Circuito? c) {
		if (c == null) return 1;

		return c.Vetores.Count.CompareTo(this.Vetores.Count);
	}

	public override int GetHashCode() {
		return base.GetHashCode();
	}

	public void AddAresta(Aresta a) {
		if (!this.HasAresta(a)) {

			if (!this.HasVetor(a.Vetor1))
				Vetores.Add(a.Vetor1);

			if (!this.HasVetor(a.Vetor2))
				Vetores.Add(a.Vetor2);

			Arestas.Add(a);
		}
	}

	public void MergeCircuit(Circuito c) {
		for (int i = 0; i < c.Vetores.Count; i++) {
			if (!this.HasVetor(c.Vetores[i])) {
				Vetores.Add(c.Vetores[i]);
			}
		}

		for (int i = 0; i < c.Arestas.Count; i++) {
			if (!this.HasAresta(c.Arestas[i])) {
				this.AddAresta(c.Arestas[i]);
			}
		}
	}

	public bool HasVetor(Vector3 v) {
		return Vetores.Contains(v);
	}

	public bool HasAresta(Aresta a) {
		return Arestas.Contains(a);
	}

	public bool HasVetors(Aresta a) {
		return this.HasVetor(a.Vetor1) || this.HasVetor(a.Vetor2);
	}

	public override string ToString() {
		return string.Join("\n", Arestas);
	}
}
