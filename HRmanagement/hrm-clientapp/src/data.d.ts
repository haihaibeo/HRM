interface Department {
    id: string;
    name: string;
}

interface Employee {
    id: string;
    name: string;
    departmentId: string;
    workHour: number;
    positionId: string;
    position?: string;
    disciplines?: string[];
    contractStart?: string;
    contractEnd?: string;
    passportNumber?: string;
    taxNumber?: string;
}

interface Position {
    id: string;
    name: string;
}