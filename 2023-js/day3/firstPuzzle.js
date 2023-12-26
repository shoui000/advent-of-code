import { readFile } from "node:fs";

readFile('./data.txt', (err, input) => {
    if (err) throw err;

    let data = input.toString('utf8').split('\n').slice(0, -1);

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

    const information = findSymbolsAndNumbers(matrix);

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

    const result = extractTrueNumbers(information);

    let sum = 0;
    result.forEach(number => {
        sum += Number(number.number);
    });
    console.log(sum)
});
