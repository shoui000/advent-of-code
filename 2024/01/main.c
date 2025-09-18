#include <stdbool.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define BUFFER_SIZE 1000

void bubbleSort(int *a, size_t n) {
  int swaps = 0;
  do {
    swaps = 0;
    for (size_t i = 0; i < n-1; i++) {
      if (a[i] > a[i+1]) {
        a[i] ^= a[i+1];
        a[i+1] ^= a[i];
        a[i] ^= a[i+1];
        swaps++;
      }
    }
  } while (swaps);
}

void firstPuzzle(int *a, int *b, int n) {

  int sum = 0;
  for (size_t i = 0; i < n; i++) {
    sum += abs(a[i]-b[i]);
  }

  printf("FirstPuzzle: The response is %d\n", sum);
}

void secondPuzzle(int *a, int *b, int n) {
  int c = 0, sum = 0;

  for (int i = 0; i < n; i++) {
    c = 0;

    for (int j = 0; j < n; j++) {
      if (b[j] > a[i]) break; 

      if (b[j] < a[i]) continue;

      c++;
    }

    sum += a[i] * c;

  }

  printf("SecondPuzzle: The response is %d\n", sum);
}

int main(int argc, char *argv[]) {
  if (argc < 2) {
    printf("Main: the option was not passed\n");
    exit(EXIT_FAILURE);
  }

  FILE *f;

  switch (atoi(argv[1])) {
    case 0:
      f = fopen("./input_test.txt", "r");
      break;
    case 1:
      f = fopen("./input.txt", "r");
      break;
    default:
      printf("Main: Wrong option\n");
      exit(EXIT_FAILURE);
  }

  if (f == NULL) {
    printf("Main: Error while reading input file\n");
    exit(EXIT_FAILURE);
  }

  size_t capacity = BUFFER_SIZE;
  int *a = malloc(capacity * sizeof(int));
  int *b = malloc(capacity * sizeof(int));

  int n = 0;
  while (true) {

    if (n >= capacity) {
      capacity += BUFFER_SIZE;
      a = realloc(a, capacity * sizeof(int));
      b = realloc(b, capacity * sizeof(int));
      if (!a || !b) {
        printf("Main: Memory allocation failed\n");
        exit(EXIT_FAILURE);
      }
    }

    if (fscanf(f, "%d %d", &a[n], &b[n]) < 2) break;
    n++;

  }
  fclose(f);

  bubbleSort(a, n);
  bubbleSort(b, n);

  firstPuzzle(a, b, n);
  secondPuzzle(a, b, n);

  free(a);
  free(b);

  exit(EXIT_SUCCESS);
}

