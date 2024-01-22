import { readFile } from "node:fs";

readFile('./data.txt', (err, input) => {
    if (err) throw err;

    const content = parseData(input);

    const nextNumbers = content.map(findNextNumber);
    const previousNumbers = content.map(findPreviousNumber);

    let firstPuzzle = 0;
    nextNumbers.forEach(x => firstPuzzle+=x);

    let secondPuzzle = 0;
    previousNumbers.forEach(x => secondPuzzle+=x);

    console.log('First Puzzle:', firstPuzzle);
    console.log('Second Puzzle:', secondPuzzle);
});

function findPreviousNumber(line){
    let allLines = findAllLines(line);

    for (let i = allLines.length-1; i > 0; i--){
        let line = allLines[i];
        let upperLine = allLines[i-1];

        let firstNumber = line[0];
        let firstUpperNumber = upperLine[0];

        let previousNumber = 0;
        previousNumber = firstUpperNumber - firstNumber;

        upperLine.unshift(previousNumber);
    }

    let firstLine = allLines[0];
    let lastNumber = firstLine[0];

    return lastNumber;
};

function findNextNumber(line){
    let allLines = findAllLines(line);

    for (let i = allLines.length-1; i > 0; i--){
        let line = allLines[i];
        let upperLine = allLines[i-1];

        let lastNumber = line[line.length-1];
        let lastUpperNumber = upperLine[upperLine.length-1];

        let nextNumber;

        nextNumber = 0;
        nextNumber = lastUpperNumber + lastNumber;

        upperLine.push(nextNumber)
    }

    let firstLine = allLines[0];
    let lastNumber = firstLine[firstLine.length-1];

    return lastNumber;
};

function findAllLines(line){
    let allLines = [line];
    let current = allLines[0];

    while (!current.every(x => x==0)){
        let newArr = [];

        for (let i = 0; i < current.length-1; i++){
            let currentNumber = current[i];
            let nextNumber = current[i+1];

            let diff = nextNumber - currentNumber;
            newArr.push(diff);
        };

        allLines.push(newArr);
        current = newArr;
    }

    return allLines;
};

function parseData(input){
    const data = input.toString('utf8')
        .split('\n')
        .slice(0, -1)
        .map(line => line.split(' ').map(Number))

    return data;
};
