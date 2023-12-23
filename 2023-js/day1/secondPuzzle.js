import { readFile } from 'node:fs';

const numbers = {
    oneight: '18',
    twone: '21',
    threeight: '38',
    fiveight: '58',
    sevenine: '78',
    eightwo: '82',
    eighthree: '83',
    nineight: '98',
    one: '1',
    two: '2',
    three: '3',
    four: '4',
    five: '5',
    six: '6',
    seven: '7',
    eight: '8',
    nine: '9',
};

readFile('./data.txt', (err, info) => {
    if (err) throw err;

    let input = info.toString('utf8').split('\n');
    let sum = 0;

    input.forEach((item) => {
        for (const [key, value] of Object.entries(numbers)){
            item = item.replaceAll(key, value);
        };

        const regex = /[0-9]/g;
        const found  = item.match(regex);
        if (found){
            const number = Number(`${found[0]}${found[found.length-1]}`);
            sum += number;
        }
    })
    console.log(sum);
});
