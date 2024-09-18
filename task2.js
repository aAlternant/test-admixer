function validSolution(board) {
    function isSetValid(set) {
        const filteredSet = set.filter((num) => num !== 0);
        return new Set(filteredSet).size === 9 && filteredSet.length === 9;
    }

    // row test
    for (let row = 0; row < 9; row++) {
        if (!isSetValid(board[row])) {
            return false;
        }
    }

    // column test
    for (let col = 0; col < 9; col++) {
        const column = [];
        for (let row = 0; row < 9; row++) {
            column.push(board[row][col]);
        }
        if (!isSetValid(column)) {
            return false;
        }
    }

    // block 3x3 test
    for (let row = 0; row < 9; row += 3) {
        for (let col = 0; col < 9; col += 3) {
            const block = [];
            for (let r = 0; r < 3; r++) {
                for (let c = 0; c < 3; c++) {
                    block.push(board[row + r][col + c]);
                }
            }
            if (!isSetValid(block)) {
                return false;
            }
        }
    }

    return true;
}
