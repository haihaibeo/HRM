import React, { Children } from 'react'

type ContextProps = {
    employees: Employee[];
    department: Department[];
}

const Context = React.createContext<ContextProps>({ department: [], employees: [] })

const ContextProvider: React.FC = ({ children }) => {
    const [context, setContext] = React.useState<ContextProps>({ department: [], employees: [] });
    React.useEffect(() => {
        getDepartmentsAsync().then(d => setContext(c => ({
            ...c,
            department: d
        })));
        getEmployeesAsync().then(e => setContext(c => ({
            ...c,
            employees: e
        })))
    }, [])

    return (
        <Context.Provider value={context}>
            {children}
        </Context.Provider>
    )
}

export default ContextProvider;

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