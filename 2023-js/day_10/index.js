import { readFile } from 'node:fs';

readFile('./testdata.txt', (err, input) => {
    if (err) throw err;

    const matrix = toMatrix(input);

    let start = findStart(matrix);
});

function toMatrix(input){
    const content = input.toString('utf8')
        .split('\n')
        .slice(0, -1)
        .map(line => line.split(''));

    return content;
};

function findStart(matrix){
    let x, y;
    matrix.forEach((line, lY) => {
        line.forEach((char, lX) => {
            if (char === 'S'){
                x = lX;
                y = lY;
            };
        });
    });

    return {x: x, y: y};
};

function findConnections(start, matrix){
    let allConnections = [];

    switch (start){
        case 'S':
            if (start.x > 0){
            } else if (start.y > 0){
            }
            break;
    };

    return allConnections;
};
