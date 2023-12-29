import { readFile } from "node:fs";

readFile('./data.txt', (err, input) => {
    if (err) throw err;

    function createMatrix(info){
        let matrix = []
        info.forEach((line, index) => {
            matrix.push([]);
            let i = 0;
            for (const char of line){
                matrix[index].push([])
                matrix[index][i].push(char)
                i++
            };
        });
        return matrix;
    };

    function findSymbolsAndNumbers(matrix){
        let info = {symbols: [], numbers: []};
        matrix.forEach((line, lIndex) => {
            let lastIndex = -2;
            line.forEach((char, cIndex) =>{
                char = char[0]
                if (char != '.' && !char.match(/[0-9]/)){
                    info.symbols.push({location: [lIndex, cIndex], char: char})
                } else if (char != '.' && char.match(/[0-9]/)) {
                    if (lastIndex == cIndex-1){
                        info.numbers[info.numbers.length-1].number += char;
                        info.numbers[info.numbers.length-1].location.push([lIndex, cIndex]);
                    } else {
                        info.numbers.push({number: char, location: [[lIndex, cIndex]]})    
                    };
                    lastIndex = cIndex;
                };
            })   
        });
        return info;
    };

    function extractTrueNumbers(info){
        let result = [];
        info.numbers.forEach(number => {
            let found = false;
            number.location.forEach(coord => {
                if (!found && findSymbol(coord, info)){
                    result.push(number)
                    found = true;
                }
            });
        });

        function findSymbol(coord, info){
            let ret = false;
            info.symbols.forEach(symbol => {
                if (symbol.location[0] == coord[0]){
                    if (symbol.location[1] == coord[1]+1 || symbol.location[1] == coord[1]-1){ret = true};
                } else if (symbol.location[0] == coord[0]+1 || symbol.location[0] == coord[0]-1){
                    if (symbol.location[1] == coord[1]) {ret = true};
                    if (symbol.location[1] == coord[1]-1 || symbol.location[1] == coord[1]+1){ret = true};
                };
            });
            return ret;
        };

        return result;
    };

    function getAllGearsRatio(info){
        const gears = info.symbols.filter(x => x.char == '*');

        const gearRatio = gears.map(gear => {
            const gearRatio = getNextNumbers(info.numbers, gear.location); 
            if (gearRatio.length == 2){
                return gearRatio;
            }
        });

        function getNextNumbers(numbers, gearCoord){
            const nextLines = numbers.filter(x => [gearCoord[0], gearCoord[0]+1, gearCoord[0]-1].includes(x.location[0][0]));
            const nextBlocks = nextLines.filter(findNextBlocks);
            function findNextBlocks(number){
                const nextBlocksCoord = [gearCoord[1], gearCoord[1]+1, gearCoord[1]-1];
                let isNext = false;
                number.location.forEach(loc => {
                    if (nextBlocksCoord.includes(loc[1])){
                        isNext = true;
                    }
                });
                return isNext;
            };
            return nextBlocks;
        };
        return gearRatio;
    };

    let data = input.toString('utf8').split('\n').slice(0, -1);

    const matrix = createMatrix(data);
    const information = findSymbolsAndNumbers(matrix);

    const result = extractTrueNumbers(information);
    const gearRatio = getAllGearsRatio(information);

    let sumFirst = 0;
    result.forEach(number => {
        sumFirst += Number(number.number);
    });

    let sumSecond = 0;
    gearRatio.forEach(pair => {
        if (pair){
            sumSecond += Number(pair[0].number) * Number(pair[1].number);
        }
    });

    console.log(`First Answer: ${sumFirst}`);
    console.log(`Second Answer: ${sumSecond}`);
});
