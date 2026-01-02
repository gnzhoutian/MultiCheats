#!/bin/bash
# usage: ./make7z.sh 1.2.0/1.2.0-debug
set -x -e
TOP_PATH=$(cd `dirname $0`; pwd); cd $TOP_PATH
rm -rf tmpbuild; mkdir -p tmpbuild

VERSION=$1  # 1.2.0-debug
VER_MINI=$(echo $1 | awk -F'-' '{print $1}')  # 1.2.0

make_mod(){
    mod_name=$1
    mod_date=$(date +%Y-%m-%d)

    mkdir -p tmpbuild
    cp -af ${TOP_PATH}/src/_Module tmpbuild/${mod_name}
    
    pushd tmpbuild/${mod_name}
        mkdir -p plugins src
        cp -af ${TOP_PATH}/src/obj/x64/Debug/MCSMultiCheats.dll plugins/
        cp -af ${TOP_PATH}/src/obj/x64/Debug/MCSMultiCheats.pdb plugins/
        
        cp -af ${TOP_PATH}/src/*.cs src/
        cp -af ${TOP_PATH}/src/*.sln src/
        cp -af ${TOP_PATH}/src/*.csproj* src/
        
        cp -af ${TOP_PATH}/pics/title.png ./
        cp -af ${TOP_PATH}/LICENSE ./
        cp -af ${TOP_PATH}/README.md ./
    popd
    
    ${TOP_PATH}/tools/7za.exe a -r ${mod_name}-${VERSION}.7z ${mod_name}
    mv -f ${mod_name}-${VERSION}.7z ${TOP_PATH}/archives/
}

mod_test(){
    dst_path="/d/Games/steam/steamapps/common/觅长生/本地Mod测试/MCSMultiCheats"
    if [ -d ${dst_path} ]; then
        pushd ${dst_path}/..
            rm -rf MCSMultiCheats
            cp -af ${TOP_PATH}/tmpbuild/MCSMultiCheats MCSMultiCheats
        popd
    fi
}

# main
if [ "$1"x != ""x ];then
    make_mod MCSMultiCheats
    mod_test
    date
else
    set +x
    echo "入参错误，示例如下："
    echo "  - 测试版本: ./make7z.sh 1.2.0-debug"
    echo "  - 正式版本: ./make7z.sh 1.2.0"
fi
