import { readFile } from "node:fs";

readFile('./data.txt', (err, input) => {
    if (err) throw err;

    let data = input.toString('utf8').split('\n').slice(0, -1);
    data = data.slice(0, 4);

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

        gears.forEach(gear => {
           getNextNumbers(info.numbers, gear.location); 
        });

        function getNextNumbers(numbers, gearCoord){
            const nextLines = numbers.filter(x => x.location[0][0] == gearCoord[0] || x.location[0][0] == gearCoord[0]+1 || x.location[0][0] == gearCoord[0]-1);
            console.log(nextLines);
            return nextLines
        };
    };

    const result = getAllGearsRatio(information);

    let sum = 0;
    result.forEach(number => {
        sum += Number(number.number);
    });
    console.log(sum)
});
