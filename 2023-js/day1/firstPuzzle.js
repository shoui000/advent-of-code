import { readFile } from 'node:fs';

readFile('./data.txt', (err, info) => {
    if (err) throw err;

    let input = info.toString('utf8').split('\n');
    let sum = 0;

    input.forEach((item) => {
        const regex = /[0-9]/g;
        const found  = item.match(regex);
        if (found){
            const number = Number(`${found[0]}${found[found.length-1]}`);
            sum += number;
        }
    })
    console.log(sum);
});
