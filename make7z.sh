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

    mkdir -p tmpbuild/pak

    pushd tmpbuild/pak
        cp -af ${TOP_PATH}/src/Libs .
        ${TOP_PATH}/tools/7za.exe a -r -tzip ../${mod_name}.pak Libs
    popd

    pushd tmpbuild
        cp -af ${TOP_PATH}/src/_Module ${mod_name}
        
        sed -i 's!MOD_NAME!'${mod_name}'!g' ${mod_name}/mod.manifest
        sed -i 's!MOD_DATE!'${mod_date}'!g' ${mod_name}/mod.manifest
        sed -i 's!MOD_VERSION!'${VER_MINI}'!g' ${mod_name}/mod.manifest
        
        mkdir -p ${mod_name}/Data
        cp -af ${mod_name}.pak ${mod_name}/Data/
        
        cp -af ${TOP_PATH}/LICENSE ./${mod_name}/
        cp -af ${TOP_PATH}/README.md ./${mod_name}/
        
        ${TOP_PATH}/tools/7za.exe a -r ${mod_name}-${VERSION}.7z ${mod_name}
        mv -f ${mod_name}-${VERSION}.7z ${TOP_PATH}/archives/
    popd
}

mod_test(){
    dst_path="/d/Games/steam/steamapps/common/KingdomComeDeliverance/Mods/KCMultiCheats/Data/KCMultiCheats.pak"
    pushd ${TOP_PATH}/src
        if [ -f ${dst_path} ]; then
            ${TOP_PATH}/tools/7za.exe a -r -tzip ${dst_path} Libs
        fi
    popd
}

# main
if [ "$1"x == "test"x ];then
    mod_test
elif [ "$1"x != ""x ];then
    make_mod KCMultiCheats
    date
else
    set +x
    echo "入参错误，示例如下："
    echo "  - 本地调试: ./make7z.sh test"
    echo "  - 测试版本: ./make7z.sh 1.2.0-debug"
    echo "  - 正式版本: ./make7z.sh 1.2.0"
fi
