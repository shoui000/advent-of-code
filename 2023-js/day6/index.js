import { readFile } from "node:fs";

readFile('./data.txt', (err, input) => {
    if (err) throw err;


})
