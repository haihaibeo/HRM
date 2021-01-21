/* eslint-disable eqeqeq */
import React from 'react'
import { Button, Col, Form, Row } from 'react-bootstrap';
import ReactDatePicker from 'react-datepicker';
import URL from './URL';
import "react-datepicker/dist/react-datepicker.css";


const getDepartmentsAsync = async () => {
    let data: Array<Department> = [];
    const response: Response = await fetch(URL + "/api/department", {
        method: "GET",
    });
    if (response.status === 200) {
        data = await response.json();
        console.log(data[0].name);
    }
    else console.log(response);
    return data;
}

const getEmployeesAsync = async () => {
    let data: Array<Employee> = [];
    const response: Response = await fetch(URL + "/api/employee", {
        method: "GET"
    });
    if (response.status === 200) {
        data = await response.json();
        console.log(data);
    }
    else console.log("error fetching employees");
    return data;
}

const getPositionsAsync = async () => {
    let data: Array<Position> = [];
    const response: Response = await fetch(URL + "/api/employee/getpositions", {
        method: "GET"
    });
    if (response.status === 200) {
        data = await response.json();
        console.log("Successfully fetching positions");
    }
    else console.log("Error fetching positions");
    return data;
}

const blankEmployee: Employee = {
    id: "", departmentId: "", name: "", workHour: 0, position: "", positionId: "", contractStart: "05 October 2020 14:48 UTC", contractEnd: "05 October 2021 14:48 UTC"
};

const Details = () => {
    // TODO : Pull departments, employees and positions to Context

    const [departments, setDepartments] = React.useState<Array<Department>>();
    const [employees, setEmployees] = React.useState<Array<Employee>>();
    const [positions, setPositions] = React.useState<Array<Position>>();

    const [currEmployee, setCurrEmployee] = React.useState<Employee>();
    const [currDepartment, setCurrDepartment] = React.useState<Department>();

    React.useEffect(() => {
        getDepartmentsAsync().then(d => setDepartments(d));
        getEmployeesAsync().then(e => setEmployees(e));
        getPositionsAsync().then(p => setPositions(p));
    }, [])

    React.useEffect(() => {
        if (departments !== undefined)
            setCurrDepartment(departments[0]);
    }, [departments])

    const empRef = React.useRef<any>(null);

    const handleChangeDpm = (e: any) => {
        const value = e.target.value;
        // eslint-disable-next-line eqeqeq
        const dpm = departments?.find(d => d.id == value);
        empRef.current.selectedIndex = 0;
        setCurrEmployee(blankEmployee);
        setCurrDepartment(dpm);
    }

    const handleChangeEpl = (e: any) => {
        const value = e.target.value;
        // eslint-disable-next-line eqeqeq
        setCurrEmployee(employees?.find(e => e.id == value));
    }

    const GetAvgWorkHour = () => {
        const assistants = employees?.filter(e => e.departmentId == currDepartment?.id && e.position === "Assistant");
        let totalHour = 0;
        assistants?.forEach(a => totalHour += a.workHour);
        if (assistants?.length !== 0 && assistants != undefined)
            return totalHour / assistants.length;
        else return 0;
    }

    return (
        <div className="container mb-1 pb-1 mt-3">
            <h3 className="mr-auto">Employee Details </h3>
            <Form>
                <Form.Group>
                    <Form.Control as="select" onChange={(e) => handleChangeDpm(e)}>
                        {departments?.map((d, key) => {
                            return <option key={key} value={d.id}>{d.name}</option>
                        })}
                    </Form.Control>
                </Form.Group>

                <div className="container">
                    {employees?.filter(e => e.departmentId == currDepartment?.id).map(ep => {
                        return <PersonDetail emp={ep} positions={positions} key={ep.id}></PersonDetail>
                    })}
                </div>


                <div className="d-flex">
                    <h3 className="mr-auto align-self-center ">Average workload of assistants: </h3>
                    <h1 className="ml-auto align-self-center ">{GetAvgWorkHour()} hour(s)</h1>
                </div>

                <br></br>
                <Form.Group className="container">
                    <Form.Label>
                        Choose employee
                    </Form.Label>
                    <Form.Control as="select" onChange={(e) => handleChangeEpl(e)} ref={empRef}>
                        <option key={blankEmployee.id}>--Choose employee--</option>
                        {employees?.filter(e => e.departmentId == currDepartment?.id).map((e, key) => {
                            return <option key={key} value={e.id}>{e.name}</option>
                        })}
                    </Form.Control>
                    <div className="container">
                        {currEmployee?.disciplines?.length === 0
                            ? <h3 className="text-danger">Empty</h3>

                            : currEmployee?.disciplines?.map(d => {
                                return <Discipline name={d}></Discipline>
                            })
                        }
                    </div>
                </Form.Group>

                <AddEmployee positions={positions} departments={departments}></AddEmployee>
            </Form>
        </div>
    )
}

type AddEmployeeProps = {
    positions?: Position[];
    departments?: Department[];
}
const AddEmployee = ({ positions, departments }: AddEmployeeProps) => {
    const [emp, setEmp] = React.useState<Employee>(blankEmployee);
    React.useEffect(() => {
        console.log(emp.contractStart?.replace(',', ''));
    }, [emp])

    const Add = async () => {
        let myHeaders = new Headers();
        myHeaders.append("Content-Type", "application/json");
        const start = emp.contractStart?.replace(',', '');
        const end = emp.contractEnd?.replace(',', '');
        console.log(start);

        let raw = JSON.stringify(
            {
                "name": emp.name,
                "passportNumber": emp.passportNumber,
                "taxNumber": emp.taxNumber,
                "departmentId": emp.departmentId,
                "positionId": emp.positionId,
                "contractStart": start,
                "contractEnd": end
            });

        const response: Response = await fetch(URL + "/api/employee", {
            method: 'POST',
            headers: myHeaders,
            body: raw,
            redirect: 'follow'
        })

        if (response.status === 200) {
            const msg = await response.json();
            console.log(msg);
        }
        else console.log("Error adding new employee");
    }

    return (
        <div>
            <h3 className="mr-auto">Add new employee </h3>
            <Form className="container">
                <Form.Group as={Row}>
                    <Form.Label column sm="3">Name</Form.Label>
                    <Col sm="9">
                        <Form.Control placeholder="Enter name" value={emp.name}
                            onChange={(event) => setEmp(emp => ({ ...emp, name: event.target.value }))} />
                    </Col>
                </Form.Group>
                <Form.Group as={Row}>
                    <Form.Label column sm="3">Passport</Form.Label>
                    <Col sm="9">
                        <Form.Control placeholder="Enter passport number" value={emp.passportNumber}
                            onChange={(event) => setEmp(emp => ({ ...emp, passportNumber: event.target.value }))} />
                    </Col>
                </Form.Group>
                <Form.Group as={Row}>
                    <Form.Label column sm="3">Tax number</Form.Label>
                    <Col sm="9">
                        <Form.Control placeholder="Enter tax number" value={emp.taxNumber}
                            onChange={(event) => setEmp(emp => ({ ...emp, taxNumber: event.target.value }))} />
                    </Col>
                </Form.Group>

                <Form.Group as={Row}>
                    <Form.Label column sm="3">Position</Form.Label>
                    <Col sm="9">
                        <Form.Control as="select" className="col-sm" onChange={(event) => { setEmp(emp => ({ ...emp, positionId: event.target.value })) }}>
                            {positions?.map((p, key) => {
                                return <option key={key} value={p.id}>{p.name}</option>
                            })}
                        </Form.Control>
                    </Col>
                </Form.Group>
                <Form.Group as={Row}>
                    <Form.Label column sm="3">Department</Form.Label>
                    <Col sm="9">
                        <Form.Control as="select" className="col-sm" onChange={(event) => { setEmp(emp => ({ ...emp, departmentId: event.target.value })) }}>
                            {departments?.map((p, key) => {
                                return <option key={key} value={p.id}>{p.name}</option>
                            })}
                        </Form.Control>
                    </Col>
                </Form.Group>
                <Form.Group as={Row}>
                    <Form.Label column sm="3">Start date of contract</Form.Label>
                    <Col sm="9" className="d-flex">
                        <ReactDatePicker className="align-self-center" selected={new Date(emp.contractStart as string)}
                            onChange={date => setEmp(emp => ({ ...emp, contractStart: date?.toLocaleString() }))} />
                    </Col>
                </Form.Group>
                <Form.Group as={Row}>
                    <Form.Label column sm="3">End date of contract</Form.Label>
                    <Col sm="9" className="d-flex">
                        <ReactDatePicker className="align-self-center" selected={new Date(emp.contractEnd as string)}
                            onChange={date => setEmp(emp => ({ ...emp, contractEnd: date?.toLocaleString() }))} />
                    </Col>
                </Form.Group>
                <Button onClick={() => Add()}>Add</Button>
            </Form>
        </div>
    )
}

type PersonDetailProps = {
    emp: Employee;
    positions?: Position[];
}

const PersonDetail = ({ emp, positions }: PersonDetailProps) => {
    const [dateStart, setDateStart] = React.useState<Date | null | [Date, Date]>();
    const [dateEnd, setDateEnd] = React.useState<Date | null | [Date, Date]>();
    const [newPosId, setNewPosId] = React.useState(emp.positionId);
    const oldPosId = emp.positionId;

    React.useEffect(() => {
        if (emp.contractStart != undefined) {
            setDateStart(new Date(emp.contractStart));
        }
        if (emp.contractEnd != undefined) {
            setDateEnd(new Date(emp.contractEnd));
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [])

    const HandleChangePos = (e: any) => {
        const value = e.target.value;
        setNewPosId(value);
    }

    const UpdateEmployee = () => {
        if (newPosId != oldPosId)
            UpdatePositionAsync(emp.id, newPosId);
    }

    return (
        <div className="d-flex border mb-1 align-items-center">
            <h4 className="col-sm">{emp.name}</h4>
            {/* <h4 className="col-sm">{emp.position}</h4> */}
            <Form.Control as="select" className="col-sm" onChange={(e) => HandleChangePos(e)}>
                {positions?.map((p, key) => {
                    return <option key={key} value={p.id} selected={p.id === emp.positionId && true}>{p.name}</option>
                })}
            </Form.Control>
            <div className="col-md">
                <ReactDatePicker selected={dateStart as Date | undefined} onChange={date => setDateStart(date)} />
                <ReactDatePicker selected={dateEnd as Date | undefined} onChange={date => setDateEnd(date)} />
            </div>
            <h4 className="col-sm">
                {IsExpired(emp.contractEnd) === undefined && "No Contract"}
                {IsExpired(emp.contractEnd) === true && "Expired"}
                {IsExpired(emp.contractEnd) === false && "Not expired"}
            </h4>
            <Button className="col-sm" onClick={() => UpdateEmployee()}>Update</Button>
        </div>
    )
}

const UpdatePositionAsync = async (empId: number | string, posId: number | string) => {
    let myHeaders = new Headers();
    myHeaders.append("Content-Type", "application/json");

    const raw = JSON.stringify({ "EmployeeId": empId, "PositionId": posId });

    const response: Response = await fetch(URL + "/api/employee/change-pos", {
        method: 'POST',
        headers: myHeaders,
        body: raw,
        redirect: 'follow'
    });

    if (response.status === 200) console.log("Position updated");
    else console.log("Error updating position");
}

type DisciplineProps = {
    name: string;
}
const Discipline = ({ name }: DisciplineProps) => {
    return (
        <div className="row border mb-1">
            <h3 className="col-sm">{name}</h3>
        </div>
    )
}

const IsExpired = (date?: string) => {
    if (date == undefined) return undefined;
    const now = new Date(Date.now());
    const contractDate = new Date(date);
    if (contractDate >= now) return false;
    else
        return true;
}

export default Details;