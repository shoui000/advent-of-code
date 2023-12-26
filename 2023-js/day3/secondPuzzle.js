import { readFile } from "node:fs";

readFile('./data.txt', (err, input) => {
    if (err) throw err;

    let data = input.toString('utf8').split('\n').slice(0, -1);
    // data = data.slice(0, 4);

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

    const matrix = createMatrix(data);

    function findSymbolsAndNumbers(matrix){
        let info = {symbols: [], numbers: []};
        let id = 0;
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
                        info.numbers.push({number: char, location: [[lIndex, cIndex]], id: id}); 
                        id++
                    };
                    lastIndex = cIndex;
                };
            })   
        });
        return info;
    };

    const information = findSymbolsAndNumbers(matrix);

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

    const result = getAllGearsRatio(information);

    let sum = 0;
    result.forEach(pair => {
        if (pair){
            sum += Number(pair[0].number) * Number(pair[1].number);
        }
    });
    console.log(sum)
});
